<%@ Page Language="C#" AutoEventWireup="true" CodeFile="message.aspx.cs" Inherits="index" %>

<!DOCTYPE html>
<meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;">
<script src="JS/jquery-3.3.1.js"></script>
<script type="text/javascript">
    var ws = null;
    $(function () {
        if ("WebSocket" in window) {
            var url = "ws://127.0.0.1:1234";
            ws = new WebSocket(url);
            ws.onopen = function () {
                //alert("Connected");
            };
            ws.onmessage = function (evt) {
                var received_msg = evt.data;
                ResultSend(received_msg);
                //alert("Received " + received_msg)
            };
            ws.onclose = function () {
                //alert("Closed");
            }
            ws.onerror = function () {
                alert("Error");
            }


            $('#CloseBtn').click(function (event) {
                ws.close();
            });
        }
        else {
            alert("WebSocket NOT supported by your Browser!");
        }
    });

    //function Send(event) {
    //    event.preventDefault();
    //    console.log($('#MyInput').val());
    //    ws.send($('#MyInput').val());
    //}
</script>

<script type="text/javascript">

    function GetQuery(name, empty) {
        var sallvars = window.location.search.substring(1);
        var svars = sallvars.split("&");
        for (i = 0; i < svars.length; i++) {
            var svar = svars[i].split("=");
            if (svar[0] == name) return decodeURI(svar[1]);
        }
        return empty;
        //var reg = new regexp("(^|&)" + name + "=([^&]*)(&|$)");
        //var r = window.location.search.substr(1).match(reg);
        //if (r != null)
        //    return decodeuri(r[2]);
        //return "";

    }
    const _name = GetQuery('name');
    const _group_id = GetQuery('group_id');
    $(function () {
        $('#title_name').text(_name + '的聊天室');

        SelectMessage();

        function SelectMessage() {
            $('#content-div').html('');
            var sArg = {
                "method": "SelectMessage",
                "group_id": _group_id
            };

            $.ajax({
                url: 'Handler/ClientHandler.ashx',
                data: sArg,
                async: true,
                contentType: 'application/json; charset=UTF-8',
                dataType: "json",
                success: function (data) {
                    ResultSelectMessage(data);
                },
                error: function (sError) {
                    alert('錯誤訊息：不知名錯誤，跳往主頁面。');
                    window.location = 'index.aspx';
                }
            });
        }

        function ResultSelectMessage(data) {
            let jsonValues = jQuery.parseJSON(JSON.stringify(data));
            for (let index = 0; index < jsonValues.length; index++) {
                var jsonValue = jQuery.parseJSON(JSON.stringify(jsonValues[index]));
                AppendMessage(jsonValue);
            }
            //$('#content-div').html(html.join(''));
            ContentEvent();
            GotoBottom();
        }
    });

    function AppendMessage(data) {
        let pos = (data.type == "0") ? 'right' : 'left';
        let html = [];
        html.push(" <div style='padding: 10px; width: calc(100% - 20px); float: left;'> ");
        html.push(" <div style='float: " + pos + "; display: block;'> ");
        html.push(" <img src='ListImage/Pokemon_112.png' style='width: 40px; height: 40px; border-radius: 25px; border: 1px solid #808080; padding: 5px;' /> ");
        html.push(" <div style='text-align: center; width: 50px;'>" + data.name + "</div> ");
        html.push(" </div> ");
        html.push(" <span style='float: " + pos + "; margin-" + pos + ": 5px; word-break: break-all; width: calc(100% - 100px); border: 1px solid #e9e9e9; border-radius: 5px; padding: 10px;'>" + data.message + "</span> ");
        html.push(" </div> ");
        $('#content-div').append(html.join(''));
    }

    function ContentEvent() {
        $('#content-div').scroll(function () {
            let offset = $('#content-div').prop('scrollHeight') - $('#content-div').height() - 400;
            if ($(this).scrollTop() < offset) {
                $('#btn_go_bottom').fadeIn(222);
            } else {
                $('#btn_go_bottom').stop().fadeOut(222);
            }
        }).scroll();
    }

    function Send() {
        var sArg = {
            "method": "InsertMessage",
            "group_id": _group_id,
            "message": $('#input_text').val(),
        };

        $.ajax({
            url: 'Handler/ClientHandler.ashx',
            data: sArg,
            async: true,
            contentType: 'application/json; charset=UTF-8',
            dataType: "json",
            success: function (data) {
                var datas = "InsertMessage," + _group_id + "," + data.id + "," + $('#input_text').val() + "," + data.name;
                ws.send(datas);
                //ResultSend(data);
            },
            error: function (sError) {
                alert('錯誤訊息：不知名錯誤，跳往主頁面。');
                window.location = 'index.aspx';
            }
        });
    }

    function ResultSend(data) {
        var jsonValues = jQuery.parseJSON(data);
        if(jsonValues.msg == "Y" && jsonValues.group_id == _group_id) {
            AppendMessage(jsonValues);
            $('#input_text').val('');
            GotoBottom();
        }
    }

    function GotoBottom() {
        let offset = $('#content-div').prop('scrollHeight') - $('#content-div').height();
        $('#content-div').animate({ scrollTop: offset }, 333);
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/message.css" rel="stylesheet" />
</head>
<body>
    <div id="title-div">
        <div style="float: left;">
            <img src="Image/back.png" onclick="location='index.aspx'" style="width: 20px; height: 20px; cursor: pointer;" onmouseout="this.src='Image/back.png'" onmouseover="this.src='Image/back_1.png'" />
        </div>
        <div id="title_name">聊天室</div>
    </div>
    <div id="content-div">
        <div style="padding: 10px; width: calc(100% - 20px); float: left;">
            <div style="float: left; display: block;">
                <img src="ListImage/Pokemon_112.png" style="width: 40px; height: 40px; border-radius: 25px; border: 1px solid #808080; padding: 5px;" />
                <div style="text-align: center; width: 50px;">asd</div>
            </div>
            <span style="float: left; margin-left: 5px; word-break: break-all; width: calc(100% - 100px); border: 1px solid #e9e9e9; border-radius: 5px; padding: 10px;">進入 DiffNow 網站後會有左右兩個對話視窗，將要比較的純文字內容或是程式碼貼上，從 Document Type 選取文件內容，按下 Compare 即可比對。如果你想直接上傳檔案，可以選擇 File Upload，或使用 URLs 輸入檔案網址就能快速比較檔案。</span>
        </div>


        <%--        <div style="text-align: right; position: relative; padding: 10px; width: calc(100% - 20px);">
            <img src="ListImage/Pokemon_112.png" style="width: 40px; height: 40px; border-radius: 25px; border: 1px solid #808080; padding: 5px;" />
            <span style="right: 0; position: absolute; bottom: -10px; text-align: center; margin-right: 10px; width: 50px;">asd</span>
            <span style="right: 0; position: absolute; top: 10px; word-break: break-all; width: calc(100% - 150px); margin-right: 70px; border: 1px solid #e9e9e9; border-radius: 5px; padding: 10px;">進入 DiffNow 網站後會有左右兩個對話視窗，將要比較的純文字內容或是程式碼貼上，從 Document Type 選取文件內容，按下 Compare 即可比對。如果你想直接上傳檔案，可以選擇 File Upload，或使用 URLs 輸入檔案網址就能快速比較檔案。</span>
        </div>--%>

        <div style="padding: 10px; width: calc(100% - 20px); float: left;">
            <div style="float: right; display: block;">
                <img src="ListImage/Pokemon_112.png" style="width: 40px; height: 40px; border-radius: 25px; border: 1px solid #808080; padding: 5px;" />
                <div style="text-align: center; width: 50px;">asd</div>
            </div>
            <span style="float: right; margin-right: 5px; word-break: break-all; width: calc(100% - 100px); border: 1px solid #e9e9e9; border-radius: 5px; padding: 10px;">進入 DiffNow 網站後會有左右兩個對話視窗，將要比較的純文字內容或是程式碼貼上，從 Document Type 選取文件內容，按下 Compare 即可比對。如果你想直接上傳檔案，可以選擇 File Upload，或使用 URLs 輸入檔案網址就能快速比較檔案。</span>
        </div>

        <div style="padding: 10px; width: calc(100% - 20px); float: left;">
            <div style="float: right; display: block;">
                <img src="ListImage/Pokemon_112.png" style="width: 40px; height: 40px; border-radius: 25px; border: 1px solid #808080; padding: 5px;" />
                <div style="text-align: center; width: 50px;">asd</div>
            </div>
            <span style="float: right; margin-right: 5px; word-break: break-all; width: calc(100% - 100px); border: 1px solid #e9e9e9; border-radius: 5px; padding: 10px;">進入 DiffNow 網站後會有左右兩個對話視窗，將要比較的純文字內容或是程式碼貼上，從 Document Type 選取文件內容，按下 Compare 即可比對。如果你想直接上傳檔案，可以選擇 File Upload，或使用 URLs 輸入檔案網址就能快速比較檔案。</span>
        </div>

        <div style="padding: 10px; width: calc(100% - 20px); float: left;">
            <div style="float: right; display: block;">
                <img src="ListImage/Pokemon_112.png" style="width: 40px; height: 40px; border-radius: 25px; border: 1px solid #808080; padding: 5px;" />
                <div style="text-align: center; width: 50px;">asd</div>
            </div>
            <span style="float: right; margin-right: 5px; word-break: break-all; width: calc(100% - 100px); border: 1px solid #e9e9e9; border-radius: 5px; padding: 10px;">進入 DiffNow 網站後會有左右兩個對話視窗，將要比較的純文字內容或是程式碼貼上，從 Document Type 選取文件內容，按下 Compare 即可比對。如果你想直接上傳檔案，可以選擇 File Upload，或使用 URLs 輸入檔案網址就能快速比較檔案。</span>
        </div>

        <div style="padding: 10px; width: calc(100% - 20px); float: left;">
            <div style="float: right; display: block;">
                <img src="ListImage/Pokemon_112.png" style="width: 40px; height: 40px; border-radius: 25px; border: 1px solid #808080; padding: 5px;" />
                <div style="text-align: center; width: 50px;">asd</div>
            </div>
            <span style="float: right; margin-right: 5px; word-break: break-all; width: calc(100% - 100px); border: 1px solid #e9e9e9; border-radius: 5px; padding: 10px;">進入 DiffNow 網站後會有左右兩個對話視窗，將要比較的純文字內容或是程式碼貼上，從 Document Type 選取文件內容，按下 Compare 即可比對。如果你想直接上傳檔案，可以選擇 File Upload，或使用 URLs 輸入檔案網址就能快速比較檔案。</span>
        </div>

        <div style="padding: 10px; width: calc(100% - 20px); float: left;">
            <div style="float: right; display: block;">
                <img src="ListImage/Pokemon_112.png" style="width: 40px; height: 40px; border-radius: 25px; border: 1px solid #808080; padding: 5px;" />
                <div style="text-align: center; width: 50px;">asd</div>
            </div>
            <span style="float: right; margin-right: 5px; word-break: break-all; width: calc(100% - 100px); border: 1px solid #e9e9e9; border-radius: 5px; padding: 10px;">進入 DiffNow 網站後會有左右兩個對話視窗，將要比較的純文字內容或是程式碼貼上，從 Document Type 選取文件內容，按下 Compare 即可比對。如果你想直接上傳檔案，可以選擇 File Upload，或使用 URLs 輸入檔案網址就能快速比較檔案。</span>
        </div>

        <div style="padding: 10px; width: calc(100% - 20px); float: left;">
            <div style="float: right; display: block;">
                <img src="ListImage/Pokemon_112.png" style="width: 40px; height: 40px; border-radius: 25px; border: 1px solid #808080; padding: 5px;" />
                <div style="text-align: center; width: 50px;">asd</div>
            </div>
            <span style="float: right; margin-right: 5px; word-break: break-all; width: calc(100% - 100px); border: 1px solid #e9e9e9; border-radius: 5px; padding: 10px;">進入 DiffNow 網站後會有左右兩個對話視窗，將要比較的純文字內容或是程式碼貼上，從 Document Type 選取文件內容，按下 Compare 即可比對。如果你想直接上傳檔案，可以選擇 File Upload，或使用 URLs 輸入檔案網址就能快速比較檔案。</span>
        </div>
    </div>
    <div id="input-div">
        <div style="width: calc(100% - 100px); float: left;">
            <%--<input class="input" />--%>
            <textarea class="input" id="input_text" placeholder="請輸入..."></textarea>
        </div>
        <div style="width: 100px; float: left;">
            <div class="button buttun-blue" onclick="Send();">送出</div>
        </div>
    </div>
    <div id="btn_go_bottom" onclick="GotoBottom();">底部</div>
    <div class="action-div hide" id="action_div">
        <form class="content">
            <h3 id="action_title">新增</h3>
            <input class="input-text1" id="action_content" required="required" type="text" placeholder="名稱" />
            <div style="margin-top: 10px; width: 100%;">
                <div class="col-6">
                    <input class="button buttun-green" id="action_btn" value="確定" type="submit" onclick="Action(event);" />
                </div>
                <div class="col-6">
                    <input class="button buttun-red" onclick="CloseAction();" value="取消" type="button" />
                </div>
            </div>
        </form>
    </div>
</body>
</html>
