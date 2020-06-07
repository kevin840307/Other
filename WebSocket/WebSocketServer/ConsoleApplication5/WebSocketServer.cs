using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
    public delegate void ClientConnectedHandler(WebSocketConnection sender, EventArgs ev);

    public class WebSocketServer
    {
        // Server端的Socket
        private Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        // SHA1加密
        private SHA1 _sha1 = SHA1CryptoServiceProvider.Create();
        // WebSocket專用GUID                          
        private static readonly String GUID = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
        // 儲存所有Client連線的佇列             
        private static List<WebSocketConnection> _connections = new List<WebSocketConnection>();
        // 建立連線後觸發的事件

        public event ClientConnectedHandler OnConnected;
        private const string Host = "mndtghost.zapto.org";
        private string IP = "mndtghost.zapto.org";
        private const int PORT = 1234;

        public void Start()
        {
            IPHostEntry hostinfo = Dns.GetHostEntry(IP);
            IPAddress[] aryIP = hostinfo.AddressList;

            if (aryIP.Length > 0)
            {
                IP = aryIP[0].ToString();
            }

            // 啟動Server Socket並監聽 IPAddress.Any
            _serverSocket.Bind(new IPEndPoint(aryIP[0], PORT));
            _serverSocket.Listen(128);
            // Server Socket準備接收Client端連線
            _serverSocket.BeginAccept(new AsyncCallback(onConnect), null);
        }

        private void onConnect(IAsyncResult result)
        {
            var clientSocket = _serverSocket.EndAccept(result);
            // 進行ShakeHand動作
            ShakeHands(clientSocket);
        }

        private void ShakeHands(Socket socket)
        {
            // 存放Request資料的Buffer
            byte[] buffer = new byte[1024];
            // 接收的Request長度
            var length = socket.Receive(buffer);
            // 將buffer中的資料解碼成字串
            var data = Encoding.UTF8.GetString(buffer, 0, length);
            Console.WriteLine(data);

            // 將資料字串中的空白位元移除
            var dataArray = data.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            // 從Client傳來的Request Header訊息中取
            var key = dataArray.Where(s => s.Contains("Sec-WebSocket-Key: ")).Single().Replace("Sec-WebSocket-Key: ", String.Empty).Trim();
            var acceptKey = CreateAcceptKey(key);
            // WebSocket Protocol定義的ShakeHand訊息
            var handShakeMsg =
         "HTTP/1.1 101 Switching Protocols\r\n" +
         "Upgrade: websocket\r\n" +
         "Connection: Upgrade\r\n" +
         "Sec-WebSocket-Accept: " + acceptKey + "\r\n\r\n";

            socket.Send(Encoding.UTF8.GetBytes(handShakeMsg));

            Console.WriteLine(handShakeMsg);
            // 產生WebSocketConnection實體並加入佇列中管理
            var clientConn = new WebSocketConnection(socket);
            _connections.Add(clientConn);
            // 註冊Disconnected事件
            clientConn.OnDisconnected += new ClientDisconnectedEventHandler(DisconnectedWork);

            // 確認Connection是否繼續存在，並持續監聽
            if (OnConnected != null)
                OnConnected(clientConn, EventArgs.Empty);
            _serverSocket.BeginAccept(new AsyncCallback(onConnect), null);
        }

        public static void SendAll(string data, WebSocketConnection send)
        {
            for (int index = 0; index < _connections.Count; index++)
            {
                if (_connections[index] != send)
                {
                    _connections[index].Send(data);
                }
            }
        }

        private void DisconnectedWork(WebSocketConnection sender, EventArgs ev)
        {
            _connections.Remove(sender);
            //sender.Close();
        }

        private String CreateAcceptKey(String key)
        {
            String keyStr = key + GUID;
            byte[] hashBytes = ComputeHash(keyStr);
            return Convert.ToBase64String(hashBytes);
        }

        private byte[] ComputeHash(String str)
        {
            return _sha1.ComputeHash(System.Text.Encoding.ASCII.GetBytes(str));
        }
    }
}
