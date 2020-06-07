using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;

/// <summary>
/// Summary description for Functions
/// </summary>
public class PublicApi
{

    public static string fnGetConStr(string sLsConn)
    {
        return System.Web.Configuration.WebConfigurationManager.ConnectionStrings[sLsConn].ConnectionString;
    }

    // 將DataTable資料序列化
    public static string fnGetJson(System.Data.DataTable dtData)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serObj = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> lsData = new List<Dictionary<string, object>>();
        Dictionary<string, object> dicMap;
        foreach (DataRow dr in dtData.Rows)
        {
            dicMap = new Dictionary<string, object>();
            foreach (DataColumn col in dtData.Columns)
            {
                dicMap.Add(col.ColumnName, dr[col]);
            }
            lsData.Add(dicMap);
        }
        return serObj.Serialize(lsData);
    }

    public static System.Data.DataTable fnGetDt(string sStr, string sLsConn)
    {
        System.Data.SqlClient.SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(fnGetConStr(sLsConn));
        System.Data.SqlClient.SqlDataAdapter sqldataAdapter = new System.Data.SqlClient.SqlDataAdapter(sStr, sqlConn);
        System.Data.DataTable dtData = new System.Data.DataTable();
        sqldataAdapter.Fill(dtData);
        return dtData;
    }

    public static string fnGetValue(string sSql, string sConn)
    {
        string sValue = null;
        System.Data.SqlClient.SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(fnGetConStr(sConn));
        sqlConn.Open();
        System.Data.SqlClient.SqlCommand sqlComm = null;
        sqlComm = new System.Data.SqlClient.SqlCommand(sSql, sqlConn);
        sValue = (sqlComm.ExecuteScalar() == null) ? " " : sqlComm.ExecuteScalar().ToString();
        sqlConn.Close();
        return sValue;
    }

    public static string fnAddCondition(string sColName, string sData)
    {
        if (sData != null && sData.Length > 0)
        {
            return " AND " + sColName + " LIKE '" + sData + "' ";
        }
        return "";
    }

    public static string fnRetrieveIP(HttpRequest request)
    {
        string sIp = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (sIp == null || sIp.Trim() == string.Empty)
        {
            sIp = request.ServerVariables["REMOTE_ADDR"];
        }
        return sIp;
    }

    public static string fnExecuteSQL(string sSql, string sConn)
    {
        System.Data.SqlClient.SqlConnection conn = null;
        conn = new System.Data.SqlClient.SqlConnection(fnGetConStr(sConn));
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

    public static void fnReadLoginCookie(HttpContext context)
    {
        var reData = HttpContext.Current.Request.Cookies["login"];
        if (reData != null)
        {
            context.Session["id"] = reData["id"];
            context.Session["account_id"] = reData["account_id"];
            context.Session["password"] = reData["password"];
            context.Session["name"] = reData["name"];
        }
    }

   public static string fnInsertSql(DataTable dtData, string sSql, string[] sColumnName)
    {
        int[] iColumnIndex = new int[sColumnName.Length];
        string sMsg = "Y";
        string sSaveSql = "";

        for (int iCIndex = 0; iCIndex < sColumnName.Length; iCIndex++)
        {
            bool bFind = true;
            for (int iIndex = 0; bFind && iIndex < dtData.Columns.Count; iIndex++)
            {
                if (sColumnName[iCIndex] == dtData.Rows[0][iIndex].ToString())
                {
                    iColumnIndex[iCIndex] = iIndex;
                    bFind = false;
                }
            }
        }

        for (int iPos = 1; iPos < dtData.Rows.Count; iPos++)
        {
            sSaveSql = sSql;
            if (dtData.Rows[iPos][0].ToString().Replace(" ", "").Length > 0)
            {
                for (int iCIndex = 0; iCIndex < sColumnName.Length; iCIndex++)
                {
                    sSaveSql = sSaveSql.Replace("{" + sColumnName[iCIndex] + "}", dtData.Rows[iPos][iColumnIndex[iCIndex]].ToString());
                }
                string sExecutMessage = fnExecuteSQL(sSaveSql, "MNDT");
                if (sExecutMessage != "Y")
                {
                    sMsg += "匯入錯誤：第" + iPos + "筆" + " 訊息：" + sExecutMessage + "<br><br>";
                }
            }
            else
            {
                return sMsg;
            }
        }
        return sMsg;
    }

    public static string fnNullChange(object objData)
    {
        return fnNullChange(objData, "");
    }

    public static string fnNullChange(object objData, string sData)
    {
        return (objData == null || objData.ToString() == "") ? sData : objData.ToString();
    }


    public static void fnSetZeroOrder()
    {
        string sSql =
                      "  UPDATE [MNDTkind_details]  " +
                      "     SET [parameter] = '0'  " +
                      " WHERE [kind_id] = 'O01' " +
                      "   AND [code_id] IN ('A01', 'A02', 'B01', 'B02') ";
        sSql +=
                "  UPDATE [MNDTkind_details]  " +
                "     SET [parameter] = '" + DateTime.Now.ToString("yyyyMMdd") + "'  " +
                " WHERE [kind_id] = 'O01' " +
                "   AND [code_id] = 'DATE' ";

        PublicApi.fnExecuteSQL(sSql, "MNDT");
    }

    private static string fnFixOrder(string sNum)
    {
        for(int iIndex = sNum.Length; iIndex < 3; iIndex++)
        {
            sNum = "0" + sNum;
        }
        return sNum;
    }

}