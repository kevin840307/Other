<%@ WebHandler Language="C#" Class="ClientHandler" %>

using System;
using System.Web;
using System.Data;

public class ClientHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        fnReadLoginCookie(context);
        string sMethod = context.Request.QueryString["method"];
        sMethod = (sMethod == null) ? context.Request.Form["method"] : sMethod;
        if (sMethod != null)
        {
            if (context.Session["id"] == null && sMethod != "Login")
            {
                context.Response.Write("{ \"msg\":\"N\"}");
                return;
            }

            if (context.Session["id"] != null && sMethod == "Check")
            {
                context.Response.Write("{ \"msg\":\"Y\"}");
                return;
            }

            System.Reflection.MethodInfo methodInfo = this.GetType().GetMethod(sMethod);
            if (methodInfo != null)
            {
                methodInfo.Invoke(this, new object[] { context });
            }
        }
    }

    public void SelectMyGroup(HttpContext context)
    {
        string name = context.Request.QueryString["name"];
        if (name == null || name.Length == 0)
        {
            name = "%";
        }
        string sql = "  SELECT [id]  " +
                        "        ,[name]  " +
                        "        ,[user_id]  " +
                        "    FROM [MNDTgroup]  " +
                        "    WHERE [user_id] = '" + context.Session["id"] + "'  " +
                        "       AND name LIKE '" + name + "%' ";
        DataTable data = PublicApi.fnGetDt(sql, "MNDT");
        context.Response.Write(PublicApi.fnGetJson(data));
    }

    public void SelectGroup(HttpContext context)
    {
        string sql = "  SELECT [id]  " +
                        "        ,[name]  " +
                        "        ,[user_id]  " +
                        "    FROM [MNDTgroup]  ";
        DataTable data = PublicApi.fnGetDt(sql, "MNDT");
        context.Response.Write(PublicApi.fnGetJson(data));
    }

    public void InsertGroup(HttpContext context)
    {
        string name = context.Request.QueryString["name"];
        string msg = "Y";
        string sql = "  INSERT INTO [MNDTgroup]  " +
                        "             ([name]  " +
                        "             ,[user_id]  " +
                        "             ,[create_datetime])  " +
                        "       VALUES  " +
                        "             ('" + name + "'  " +
                        "             ,'" + context.Session["id"] + "'  " +
                        "             ,GETDATE())  ";
        msg = PublicApi.fnExecuteSQL(sql, "MNDT");
        context.Response.Write("{ \"msg\":\"" + msg + "\"}");
    }

    public void UpdateGroup(HttpContext context)
    {
        string name = context.Request.QueryString["name"];
        string id = context.Request.QueryString["id"];
        string msg = "Y";
        string sql = "  UPDATE [MNDTgroup]  " +
                        "     SET [name] = '" + name + "'  " +
                        "   WHERE  [user_id] = '" + context.Session["id"] + "' " +
                        "   AND [id] = '" + id + "' ";
        msg = PublicApi.fnExecuteSQL(sql, "MNDT");
        context.Response.Write("{ \"msg\":\"" + msg + "\"}");
    }

    public void DeleteGroup(HttpContext context)
    {
        string name = context.Request.QueryString["name"];
        string id = context.Request.QueryString["id"];
        string msg = "Y";
        string sql = "  DELETE [MNDTgroup]  " +
                        "   WHERE  [user_id] = '" + context.Session["id"] + "' " +
                        "   AND [id] = '" + id + "' ";
        msg = PublicApi.fnExecuteSQL(sql, "MNDT");
        context.Response.Write("{ \"msg\":\"" + msg + "\"}");
    }

    public void SelectMessage(HttpContext context)
    {
        string group_id = context.Request.QueryString["group_id"];
        string sql = "  SELECT [group_msg].[id],   " +
                        "         [user].[name],   " +
                        "         [group_msg].[message],   " +
                        "         [group_msg].[create_datetime],   " +
                        "         CASE [group_msg].[user_id]   " +
                        "           WHEN '" + context.Session["id"] + "' THEN '0'   " +
                        "           ELSE '1'   " +
                        "         END [type]   " +
                        "  FROM   [mndtgroup_message] [group_msg]   " +
                        "         LEFT JOIN [mndtuser] [user]   " +
                        "                ON [group_msg].[user_id] = [user].[id]   " +
                        "  WHERE  [group_id] = '" + group_id + "'   ";
        DataTable data = PublicApi.fnGetDt(sql, "MNDT");
        context.Response.Write(PublicApi.fnGetJson(data));
    }

    public void InsertMessage(HttpContext context)
    {
        string message = context.Request.QueryString["message"];
        string group_id = context.Request.QueryString["group_id"];
        string msg = "Y";
        string sql = "  INSERT INTO [MNDTgroup_message]  " +
                    "             ([group_id]  " +
                    "             ,[user_id]  " +
                    "             ,[message]  " +
                    "             ,[create_datetime])  " +
                    "       VALUES  " +
                    "             ('" + group_id + "'  " +
                    "             ,'" + context.Session["id"] + "'  " +
                    "             ,'" + message + "'  " +
                    "             ,GETDATE())  ";
        //msg = PublicApi.fnExecuteSQL(sql, "MNDT");
        context.Response.Write("{ \"msg\":\"Y\", \"name\": \"" + context.Session["name"] + "\", \"id\": \"" + context.Session["id"] + "\" }");
    }

    public void Login(HttpContext context)
    {
        string name = context.Request.QueryString["name"];
        string password = context.Request.QueryString["password"];
        string count = PublicApi.fnGetValue(" SELECT COUNT([id]) FROM [MNDTuser] WHERE [name] = '" + name + "' AND [password] = '" + password + "' ", "MNDT");
        string msg = "Y";
        string id = "-1";
        if (count == "0")
        {
            string sql = "  INSERT INTO [MNDTuser]  " +
                            "             ([name]  " +
                            "             ,[password])  " +
                            "       VALUES  " +
                            "             ('" + name + "'  " +
                            "             ,'" + password + "')  ";
            msg = PublicApi.fnExecuteSQL(sql, "MNDT");
        }

        if (msg == "Y")
        {
            id = PublicApi.fnGetValue(" SELECT [id] FROM [MNDTuser] WHERE [name] = '" + name + "' AND [password] = '" + password + "' ", "MNDT");
            fnWriteLoginCookie(context, name, id, password);
        }

        context.Response.Write("{ \"msg\":\"" + msg + "\", \"id\":\"" + id + "\"}");
    }

    #region Client


    public void fnWriteLoginCookie(HttpContext context, string sName, string sId, string sPassword)
    {
        HttpCookie htCookie = new HttpCookie("client_login");
        htCookie.Values.Add("id", sId);
        htCookie.Values.Add("password", sPassword);
        htCookie.Values.Add("name", sName);
        htCookie.Expires = DateTime.Now.AddYears(1);
        HttpContext.Current.Response.Cookies.Add(htCookie);
        context.Session["id"] = sId;
        context.Session["password"] = sPassword;
        context.Session["name"] = sName;
    }

    public static void fnReadLoginCookie(HttpContext context)
    {
        var reData = HttpContext.Current.Request.Cookies["client_login"];
        if (reData != null)
        {
            context.Session["id"] = reData["id"];
            context.Session["password"] = reData["password"];
            context.Session["name"] = reData["name"];
        }
        else
        {
            context.Session["id"] = null;
            context.Session["password"] = null;
            context.Session["name"] = null;
        }
    }

    public void fnLogout(HttpContext context)
    {
        HttpCookie htCookie = new HttpCookie("client_login");
        htCookie.Expires = DateTime.Now.AddDays(-1);
        HttpContext.Current.Response.Cookies.Add(htCookie);
        context.Session["id"] = null;
        context.Session["password"] = null;
        context.Session["name"] = null;
        context.Response.Write("{ \"msg\":\"Y\"}");
    }


    #endregion

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}