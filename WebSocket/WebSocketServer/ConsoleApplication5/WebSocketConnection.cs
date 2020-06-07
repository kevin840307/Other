using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
    public delegate void DataReceivedEventHandler(WebSocketConnection sender, DataReceivedEventArgs ev);

    public delegate void ClientDisconnectedEventHandler(WebSocketConnection sender, EventArgs ev);

    public class WebSocketConnection
    {
        private Socket _connection = null;
        // 存放資料的buffter
        private Byte[] _dataBuffer = new Byte[256];
        public event DataReceivedEventHandler OnDataReceived;
        public event ClientDisconnectedEventHandler OnDisconnected;


        public WebSocketConnection(Socket socket)
        {
            _connection = socket;
            listen();
        }

        private void listen()
        {
            _connection.BeginReceive(_dataBuffer, 0, _dataBuffer.Length, SocketFlags.None, Read, null);
        }

        private void Read(IAsyncResult result)
        {
            var receivedSize = _connection.EndReceive(result);
            if (receivedSize > 2)
            {
                // 判斷是否為最後一個Frame(第一個bit為FIN若為1代表此Frame為最後一個Frame)，超過一個Frame暫不處理
                if (!((_dataBuffer[0] & 0x80) == 0x80))
                {
                    Console.WriteLine("Exceed 1 Frame. Not Handle");
                    return;
                }
                // 是否包含Mask(第一個bit為1代表有Mask)，沒有Mask則不處理
                if (!((_dataBuffer[1] & 0x80) == 0x80))
                {
                    Console.WriteLine("Exception: No Mask");
                    OnDisconnected(this, EventArgs.Empty);
                    return;
                }
                // 資料長度 = dataBuffer[1] - 127
                var payloadLen = _dataBuffer[1] & 0x7F;
                var masks = new Byte[4];
                var payloadData = filterPayloadData(ref payloadLen, ref masks);
                // 使用WebSocket Protocol中的公式解析資料
                for (var i = 0; i < payloadLen; i++)
                    payloadData[i] = (Byte)(payloadData[i] ^ masks[i % 4]);

                // 解析出的資料
                var content = Encoding.UTF8.GetString(payloadData);
                Console.WriteLine("Received Data: {0}", content);
                Process(content);
                // 確認是否繼續接收資料，並持續監聽
                if (OnDataReceived != null)
                    OnDataReceived(this, new DataReceivedEventArgs(content));
                listen();
            }
            else
            {
                Console.WriteLine("Receive Error Data Frame");
                if (OnDisconnected != null)
                    OnDisconnected(this, EventArgs.Empty);
            }
        }

        private void Process(string data)
        {
            string[] split = data.Split(',');
            if (split[0] == "InsertMessage")
            {
                InsertMessage(split);
            }
        }

        private void InsertMessage(string[] split)
        {
            string sql = "  INSERT INTO [MNDTgroup_message]  " +
                    "             ([group_id]  " +
                    "             ,[user_id]  " +
                    "             ,[message]  " +
                    "             ,[create_datetime])  " +
                    "       VALUES  " +
                    "             ('" + split[1] + "'  " +
                    "             ,'" + split[2] + "'  " +
                    "             ,'" + split[3] + "'  " +
                    "             ,GETDATE())  ";
            string msg = fnExecuteSQL(sql, "MNDT");
            string json = "{ \"msg\":\"" + msg + "\", \"name\": \"" + split[4] + "\", \"message\": \"" + split[3] + "\", \"group_id\": \"" + split[1] + "\", \"type\": \"0\" }";
            Send(json);
            json = "{ \"msg\":\"" + msg + "\", \"name\": \"" + split[4] + "\", \"message\": \"" + split[3] + "\", \"group_id\": \"" + split[1] + "\", \"type\": \"1\" }";
            WebSocketServer.SendAll(json, this);
        }

        public static string fnExecuteSQL(string sSql, string sConn)
        {
            System.Data.SqlClient.SqlConnection conn = null;
            conn = new System.Data.SqlClient.SqlConnection(@"Data Source=GHOST-PC\SQLEXPRESS;Initial Catalog=MNDT0003;Persist Security Info=True;User ID=sa;Password=gza997wt;Connect Timeout=150");
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

            cmd.Connection = conn;
            try
            {
                conn.Open();
                cmd.CommandText = sSql;
                cmd.ExecuteNonQuery();
                return "Y";
            }
            catch (SqlException ex)
            {
                return ex.Message.ToString();
            }
            finally
            {
                conn.Close();
            }
        }

        private Byte[] filterPayloadData(ref int length, ref Byte[] masks)
        {
            Byte[] payloadData;
            switch (length)
            {
                // 包含16 bit Extend Payload Length
                case 126:
                    Array.Copy(_dataBuffer, 4, masks, 0, 4);
                    length = (UInt16)(_dataBuffer[2] << 8 | _dataBuffer[3]);
                    payloadData = new Byte[length];
                    Array.Copy(_dataBuffer, 8, payloadData, 0, length);
                    break;
                // 包含 64 bit Extend Payload Length
                case 127:
                    Array.Copy(_dataBuffer, 10, masks, 0, 4);
                    var uInt64Bytes = new Byte[8];
                    for (int i = 0; i < 8; i++)
                    {
                        uInt64Bytes[i] = _dataBuffer[9 - i];
                    }
                    UInt64 len = BitConverter.ToUInt64(uInt64Bytes, 0);

                    payloadData = new Byte[len];
                    for (UInt64 i = 0; i < len; i++)
                        payloadData[i] = _dataBuffer[i + 14];
                    break;
                // 沒有 Extend Payload Length
                default:
                    Array.Copy(_dataBuffer, 2, masks, 0, 4);
                    payloadData = new Byte[length];
                    Array.Copy(_dataBuffer, 6, payloadData, 0, length);
                    break;
            }
            return payloadData;
        }

        public void Send(Object data)
        {
            if (_connection.Connected)
            {
                try
                {
                    // 將資料字串轉成Byte
                    var contentByte = Encoding.UTF8.GetBytes(data.ToString());
                    var dataBytes = new List<byte>();

                    if (contentByte.Length < 126)   // 資料長度小於126，Type1格式
                    {
                        // 未切割的Data Frame開頭
                        dataBytes.Add((Byte)0x81);
                        dataBytes.Add((Byte)contentByte.Length);
                        dataBytes.AddRange(contentByte);
                    }
                    else if (contentByte.Length <= 65535)       // 長度介於126與65535(0xFFFF)之間，Type2格式
                    {
                        dataBytes.Add((Byte)0x81);
                        dataBytes.Add((Byte)0x7E);              // 126
                        // Extend Data 加長至2Byte
                        dataBytes.Add((Byte)((contentByte.Length >> 8) & 0xFF));
                        dataBytes.Add((Byte)((contentByte.Length) & 0xFF));
                        dataBytes.AddRange(contentByte);
                    }
                    else                 // 長度大於65535，Type3格式
                    {
                        dataBytes.Add((Byte)0x81);
                        dataBytes.Add((Byte)0x7F);              // 127
                        // Extned Data 加長至8Byte
                        dataBytes.Add((Byte)((contentByte.Length >> 56) & 0xFF));
                        dataBytes.Add((Byte)((contentByte.Length >> 48) & 0xFF));
                        dataBytes.Add((Byte)((contentByte.Length >> 40) & 0xFF));
                        dataBytes.Add((Byte)((contentByte.Length >> 32) & 0xFF));
                        dataBytes.Add((Byte)((contentByte.Length >> 24) & 0xFF));
                        dataBytes.Add((Byte)((contentByte.Length >> 16) & 0xFF));
                        dataBytes.Add((Byte)((contentByte.Length >> 8) & 0xFF));
                        dataBytes.Add((Byte)((contentByte.Length) & 0xFF));
                        dataBytes.AddRange(contentByte);
                    }
                    _connection.Send(dataBytes.ToArray());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (OnDisconnected != null)
                        OnDisconnected(this, EventArgs.Empty);
                }
            }
        }
    }

    public class DataReceivedEventArgs : EventArgs
    {
        // OnReceive事件發生時傳入的資料字串
        public String Data { get; private set; }

        public DataReceivedEventArgs(String data)
        {
            Data = data;
        }
    }
}
