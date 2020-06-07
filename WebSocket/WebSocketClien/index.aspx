<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<!DOCTYPE html>
<meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;">
<script src="JS/jquery-3.3.1.js"></script>
<%--<script type="text/javascript">
    var ws = null;
    $(function () {
        if ("WebSocket" in window) {
            var url = "ws://192.168.88.173:1234";
            ws = new WebSocket(url);
            ws.onopen = function () {
                alert("Connected");
            };
            ws.onmessage = function (evt) {
                var received_msg = evt.data;
                alert("Received " + received_msg)
            };
            ws.onclose = function () {
                alert("Closed");
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

    function Send(event) {
        event.preventDefault();
        console.log($('#MyInput').val());
        ws.send($('#MyInput').val());
    }
</script>--%>
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
</script>
<script type="text/javascript">
    let _name = "";
    $(function() {
        _name = GetQuery('name', "");
        CheckLogin();

        function CheckLogin() {
            var sArg = {
                "method": "Check"
            };

            $.ajax({
                url: 'Handler/ClientHandler.ashx',
                data: sArg,
                async: true,
                contentType: 'application/json; charset=UTF-8',
                dataType: "json",
                success: function (data) {
                    ResultCheckLogin(data);
                },
                error: function (sError) {
                    alert('錯誤訊息：不知名錯誤，跳往主頁面。');
                    window.location = 'index.aspx';
                }
            });
        }

        function ResultCheckLogin(data) {
            if(data.msg == "N") {
                $('#login_div').slideDown();
                $('#login_div').css('display', 'flex');
            } else {
                $('#login_div').slideUp();
                SelectMyGroup();
            }
        }

        function SelectMyGroup() {
            var sArg = {
                "method": "SelectMyGroup",
                "name": _name
            };

            $.ajax({
                url: 'Handler/ClientHandler.ashx',
                data: sArg,
                async: true,
                contentType: 'application/json; charset=UTF-8',
                dataType: "json",
                success: function (data) {
                    ResultSelectMyGroup(data);
                    SelectGroup();
                },
                error: function (sError) {
                    alert('錯誤訊息：不知名錯誤，跳往主頁面。');
                    window.location = 'index.aspx';
                }
            });
        }

        function ResultSelectMyGroup(data) {
            let html = [];
            let jsonValues = jQuery.parseJSON(JSON.stringify(data));
            for (let index = 0; index < jsonValues.length; index++) {
                var jsonValue = jQuery.parseJSON(JSON.stringify(jsonValues[index]));
                html.push(" <div class='col-3'> ");
                html.push(" <div class='list-item button-2' onclick='OpenMessage(\"" + jsonValue.id + "\", \"" + jsonValue.name + "\")'> ");
                html.push(" <img src='ListImage/Pokemon_112.png' style='float: left; width: 40px; height: 40px; border-radius: 25px; border: 1px solid #808080; padding: 5px;' /> ");
                html.push(" <label class='center-lab'>" + jsonValue.name + "</label> ");
                html.push(" </div> ");
                html.push(" <div class='edit' onclick='OpenEditAction(\"" + jsonValue.id + "\", \"" + jsonValue.name + "\");'> 編輯</div> ");
                html.push(" <div class='delete' onclick='DeleteGroup(\"" + jsonValue.id + "\");'>刪除</div> ");
                html.push(" </div> ");
            }
            $('#group_list_div').html(html.join(''));
        }

        function SelectGroup() {
            var sArg = {
                "method": "SelectGroup"
            };

            $.ajax({
                url: 'Handler/ClientHandler.ashx',
                data: sArg,
                async: true,
                contentType: 'application/json; charset=UTF-8',
                dataType: "json",
                success: function (data) {
                    ResultSelectGroup(data);
                },
                error: function (sError) {
                    alert('錯誤訊息：不知名錯誤，跳往主頁面。');
                    window.location = 'index.aspx';
                }
            });
        }

        function ResultSelectGroup(data) {
            let html = [];
            let jsonValues = jQuery.parseJSON(JSON.stringify(data));
            for (let index = 0; index < jsonValues.length; index++) {
                var jsonValue = jQuery.parseJSON(JSON.stringify(jsonValues[index]));
                html.push(" <div class='col-3'> ");
                html.push(" <div class='list-item' onclick='OpenMessage(\"" + jsonValue.id + "\", \"" + jsonValue.name + "\")'> ");
                html.push(" <img src='ListImage/Pokemon_112.png' style='float: left; width: 40px; height: 40px; border-radius: 25px; border: 1px solid #808080; padding: 5px;' /> ");
                html.push(" <label class='center-lab'>" + jsonValue.name + "</label> ");
                html.push(" </div> ");
                html.push(" </div> ");
            }
            $('#message_list_div').html(html.join(''));
        }
    });
</script>

<script type="text/javascript">
    let _tab = 'tab1';
    let _actionType = 0;
    let _id = '';

    
    function OpenMessage(group_id, name) {
        location = 'message.aspx?group_id=' + group_id + '&name=' + name;
    }

    function TabEvent(tab) {
        $('#' + _tab + "_content").addClass('hide');
        $('#' + _tab).removeClass('active');
        $('#' + tab).addClass('active');
        $('#' + tab + "_content").removeClass('hide');
        _tab = tab;
    }

    function Search() {
        location = 'index.aspx?name=' + $('#search_input').val();
    }

    function Login(event) {
        event.preventDefault();

        var sArg = {
            "method": "Login",
            "name": $('#login_name').val(),
            "password": $('#login_password').val()
        };

        $.ajax({
            url: 'Handler/ClientHandler.ashx',
            data: sArg,
            async: true,
            contentType: 'application/json; charset=UTF-8',
            dataType: "json",
            success: function (data) {
                ResultLogin(data);
            },
            error: function (sError) {
                alert('錯誤訊息：不知名錯誤，跳往主頁面。');
                window.location = 'index.aspx';
            }
        });
    }

    function ResultLogin(data) {
        if(data.msg == "Y") {
            location = 'index.aspx';
        }
    }

    function OpenInsertAction() {
        _actionType = 1;
        $('#action_title').text('新增');
        $('#action_btn').text('新增');
        $('#action_div').slideDown();
        $('#action_div').css('display', 'flex');
    }

    function OpenEditAction(id, name) {
        _actionType = 2;
        _id = id;
        $('#action_title').text('編輯');
        $('#action_btn').text('修改');
        $('#action_content').val(name);
        $('#action_div').slideDown();
        $('#action_div').css('display', 'flex');
    }

    function CloseAction() {
        $('#action_div').slideUp();
    }

    function Action(event) {
        event.preventDefault();
        let method = (_actionType == 1) ? "InsertGroup":"UpdateGroup";
        var sArg = {
            "method": method,
            "name": $('#action_content').val(),
            "id": _id
        };

        $.ajax({
            url: 'Handler/ClientHandler.ashx',
            data: sArg,
            async: true,
            contentType: 'application/json; charset=UTF-8',
            dataType: "json",
            success: function (data) {
                ResultAction(data);
            },
            error: function (sError) {
                alert('錯誤訊息：不知名錯誤，跳往主頁面。');
                window.location = 'index.aspx';
            }
        });
    }

    function DeleteGroup(id){
        if(!confirm("確定要刪除嗎?")) {
            return;
        }
        var sArg = {
            "method": "DeleteGroup",
            "id": id
        };

        $.ajax({
            url: 'Handler/ClientHandler.ashx',
            data: sArg,
            async: true,
            contentType: 'application/json; charset=UTF-8',
            dataType: "json",
            success: function (data) {
                ResultAction(data);
            },
            error: function (sError) {
                alert('錯誤訊息：不知名錯誤，跳往主頁面。');
                window.location = 'index.aspx';
            }
        });
    }

    function ResultAction(data){
        if(data.msg == "Y") {
            location = 'index.aspx';
        } else {
            alert('操作失敗');
        }
    }

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/index.css" rel="stylesheet" />
</head>
<body>
    <div id="title-div">
        聊天室
    </div>
    <div id="content-div">
        <div id="tab1_content" class="tab-content">
            <div id="search-div">
                <input type="text" placeholder="搜尋.." class="search-input" id="search_input">
                <img src="Image/search.png" class="search-button" onclick="Search();" />
            </div>
            <h3 class="col-12">
                <span>聊天室</span>
                <img src="Image/plus.png" class="plus" onclick="OpenInsertAction();" />

            </h3>
            <div id="group_list_div" class="col-12">
               
            </div>
        </div>

        <div id="tab2_content" class="tab-content hide">
            <h3 class="col-12">
                <span>聊天室</span>

            </h3>
            <div class="col-12" id="message_list_div">
                <div class="col-3">
                    <div class="list-item">
                        <img src="ListImage/Pokemon_112.png" class="img" />
                        <label class="center-lab">232</label>
                    </div>
                </div>

                <div class="col-3">
                    <div class="list-item button-1">
                        <img src="ListImage/Pokemon_112.png" style="float: left; width: 40px; height: 40px; border-radius: 25px; border: 1px solid #808080; padding: 5px;" />
                        <label class="top-lab">232</label>
                        <label class="bottom-lab">789</label>
                    </div>
                    <div style="float: left; line-height: 62px; padding: 5px; background: #ff5f5f; color: #ffffff;">刪除</div>
                </div>

                <div class="col-3">
                    <div class="list-item">
                        <img src="ListImage/Pokemon_112.png" style="float: left; width: 40px; height: 40px; border-radius: 25px; border: 1px solid #808080; padding: 5px;" />
                        <label style="line-height: 50px; margin-left: 20px;">232</label>
                    </div>
                </div>

                <div class="col-3">
                    <div class="list-item">
                        <img src="ListImage/Pokemon_112.png" style="float: left; width: 40px; height: 40px; border-radius: 25px; border: 1px solid #808080; padding: 5px;" />
                        <label style="line-height: 50px; margin-left: 20px;">232</label>
                    </div>
                </div>
                <div class="col-3">
                    <div class="list-item">
                        <img src="ListImage/Pokemon_112.png" style="float: left; width: 40px; height: 40px; border-radius: 25px; border: 1px solid #808080; padding: 5px;" />
                        <label style="line-height: 50px; margin-left: 20px;">232</label>
                    </div>
                </div>
                <div class="col-3">
                    <div class="list-item">
                        <img src="ListImage/Pokemon_112.png" style="float: left; width: 40px; height: 40px; border-radius: 25px; border: 1px solid #808080; padding: 5px;" />
                        <label style="line-height: 50px; margin-left: 20px;">232</label>
                    </div>
                </div>
                <div class="col-3">
                    <div class="list-item">
                        <img src="ListImage/Pokemon_112.png" style="float: left; width: 40px; height: 40px; border-radius: 25px; border: 1px solid #808080; padding: 5px;" />
                        <label style="line-height: 50px; margin-left: 20px;">232</label>
                    </div>
                </div>
                <div class="col-3">
                    <div class="list-item">
                        <img src="ListImage/Pokemon_112.png" style="float: left; width: 40px; height: 40px; border-radius: 25px; border: 1px solid #808080; padding: 5px;" />
                        <label style="line-height: 50px; margin-left: 20px;">232</label>
                    </div>
                </div>
                <div class="col-3">
                    <div class="list-item">
                        <img src="ListImage/Pokemon_112.png" style="float: left; width: 40px; height: 40px; border-radius: 25px; border: 1px solid #808080; padding: 5px;" />
                        <label style="line-height: 50px; margin-left: 20px;">232</label>
                    </div>
                </div>
                <div class="col-3">
                    <div class="list-item">
                        <img src="ListImage/Pokemon_112.png" style="float: left; width: 40px; height: 40px; border-radius: 25px; border: 1px solid #808080; padding: 5px;" />
                        <label style="line-height: 50px; margin-left: 20px;">232</label>
                    </div>
                </div>
            </div>
        </div>

        <div id="button-div" style="width: 100%; position: fixed; bottom: 0; left: 0; right: 0;">
            <div id='tab1' class="button active" onclick="TabEvent('tab1')">個人</div>
            <div id='tab2' class="button" onclick="TabEvent('tab2')">聊天</div>
        </div>
    </div>

    <div class="action-div hide" id="action_div">
        <form class="content">
            <h3 id="action_title">新增</h3>
            <input class="input-text1" id="action_content" required="required" type="text" placeholder="名稱" />
            <div style="margin-top: 10px; width: 100%;">
                <div class="col-6">
                    <input class="button buttun-green" id="action_btn" value="新增" type="submit" onclick="Action(event);" />
                </div>
                <div class="col-6">
                    <input class="button buttun-red" onclick="CloseAction();" value="取消" type="button" />
                </div>
            </div>
        </form>
    </div>

    <div class="action-div hide" id="login_div">
        <form class="content">
            <h3 id="H1">登入</h3>
            <input class="input-text1" id="login_name" required="required" type="text" placeholder="名稱" />
            <input class="input-text1" id="login_password" required="required" type="password" placeholder="密碼" style="margin-top: 10px;" />
            <div style="margin-top: 10px; width: 100%;">
                <input class="button buttun-green" value="登入" type="submit" onclick="Login(event);" />
            </div>
        </form>
    </div>
</body>
</html>
