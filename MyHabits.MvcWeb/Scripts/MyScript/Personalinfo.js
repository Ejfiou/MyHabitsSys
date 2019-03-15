$(function () {
    
    doload();
    
});

function doload() {
    boolpsd();//判断三个密码框是否符合规范
    MyUserInfo();//页面导入带入当前登录ID其余信息
    userbtn();//用户信息点击事件
    //选择头像按钮点击事件
   $('#filed').change(function(){
//获取input file的files文件数组;
//$('#filed')获取的是jQuery对象，.get(0)转为原生对象;
//这边默认只能选一个，但是存放形式仍然是数组，所以取第一个元素使用[0];
var file = $('#filed').get(0).files[0];
//创建用来读取此文件的对象
var reader = new FileReader();
//使用该对象读取file文件
reader.readAsDataURL(file);
//读取文件成功后执行的方法函数
reader.onload = function (e) {
    //读取成功后返回的一个参数e，整个的一个进度事件
    console.log(e);
    //选择所要显示图片的img，要赋值给img的src就是e中target下result里面
    //的base64编码格式的地址
    $('.imgshow').get(0).src = e.target.result;
    console.log(e.target.result);
}
    })
    $('#OldPsd').blur(function () {
        var password = $("#OldPsd").val();
        if (password != "") {
            $.post('/Personalinfo/UserPsd', {
                password: password,
            }, function (res) {
                if (res.success) {
                    console.log("成功");
                    //$("#nav-log").addClass("disp");
                    //$("#user_pt").removeClass("disp");
                    $("#OldPsd").parent().next().children().eq(0).removeClass("disp");
                    $("#OldPsd").parent().next().children().eq(1).addClass("disp");
                    //$("#psdbtn").attr("disabled", false).css("border-color", "var(--main-color)")
                    //    .css("background", "transparent")
                    //    .css("color", "var(--main-color)");
                    //$("#psdbtn").hover(function () {
                    //    $("#psdbtn").css("background", "var(--main-color)")
                    //        .css("color", "#fff");
                    //}, function () {
                    //    $("#psdbtn").css("background", "transparent")
                    //        .css("color", "var(--main-color)");
                    //});
                }
                else {
                    console.log("失败");
                    $("#OldPsd").parent().next().children().eq(1).removeClass("disp");
                    $("#OldPsd").parent().next().children().eq(0).addClass("disp");
                    //$("#psdbtn").attr("disabled", true).css("border-color", "darkgray")
                    //    .css("background", "darkgray")
                    //    .css("color", "#fff");
                }
            });
            return false;
        } else {
            $("#OldPsd").parent().next().children().eq(1).removeClass("disp");
            $("#OldPsd").parent().next().children().eq(0).addClass("disp");
            $("#psdbtn").attr("disabled", true).css("border-color", "darkgray")
                .css("background", "darkgray")
                .css("color", "#fff");
        }
    });


    //修改头像按钮点击事件
    $('#psdbtn').click(function () {
        console.log("我点击啦");
        var pwd2 = $("#Newpsd2").val();
        var url = location.search;
        console.log(url);
        var id = parseInt(url.substr(4));
        console.log(id);
        console.log(typeof (id));
        $.post('/Personalinfo/UpdatePwdInfo', {

            password: pwd2,
        }, function (res) {
            console.log(res);
            if (res.success) {
                window.location.href = "/Jumppage/Jumppage?id=" + id + "?page=/Personalinfo/Personalinfo" + "?msg=2";
            }
        });
    });
    //新密码框输入事件
    $("#Newpsd").keyup(function () {
        console.log("1");
        var pwd = $("#Newpsd").val();
        if (/^[\w\`\!\@\#\$\%\^&\*\(\)\.\,\+\-\<\>\\\|\/\:\;\"\"\'\'\~\?\[\]\{\}]{4,18}$/.test(pwd)) {
            $("#Newpsd").parent().next().children().eq(0).removeClass("disp");
            $("#Newpsd").parent().next().children().eq(1).addClass("disp");
            $("#changePwd").attr("disabled", false);
            return true;
        }
        else {
            $("#Newpsd").parent().next().children().eq(1).removeClass("disp");
            $("#Newpsd").parent().next().children().eq(0).addClass("disp");
            return false;
        }
    });
    //确认密码框输入事件
    $("#Newpsd2").keyup(function () {
        
        var pwd = $("#Newpsd").val();
        var pwd2 = $("#Newpsd2").val();
        if (pwd == "") {
            $("#Newpsd").focus();
        } else {
        if (pwd2 == pwd) {
            $("#Newpsd2").parent().next().children().eq(0).removeClass("disp");
            $("#Newpsd2").parent().next().children().eq(1).addClass("disp");
            $("#psdbtn").attr("disabled", false);

        } else {
            $("#Newpsd2").parent().next().children().eq(1).removeClass("disp");
            $("#Newpsd2").parent().next().children().eq(0).addClass("disp");
        }
        boolpsd();
        }
    });
}
//判定三个密码框是否都符合规则
function boolpsd() {
    var pwd1yes = $(".yes");
    console.log(pwd1yes);
    var index = 3;
    for (var i = 0; i < pwd1yes.length; i++) {
        console.log($(pwd1yes[i]).is('.disp'));
        if ($(pwd1yes[i]).is('.disp')) {
            index--;
        }
        console.log(index);
        if (index == 3) {
            $("#psdbtn").attr("disabled", false).css("border-color", "var(--main-color)")
                .css("background", "transparent")
                .css("color", "var(--main-color)");
            $("#psdbtn").hover(function () {
                $("#psdbtn").css("background", "var(--main-color)")
                    .css("color", "#fff");
            }, function () {
                $("#psdbtn").css("background", "transparent")
                    .css("color", "var(--main-color)");
            });

        } else {
            $("#psdbtn").attr("disabled", true).css("border-color", "darkgray")
                .css("background", "darkgray")
                .css("color", "#fff");
        }
    }
}
//修改
function MyUserInfo() {
    var url = location.search;
    console.log(url);
    var id = parseInt(url.substr(4));
    console.log(id);
    console.log(typeof (id));
    $.post('/Personalinfo/GetMyuserInfo', {
        ID: id,
    }, function (res) {
        console.log(res);
        var nickName = "用户" + id;
        if (res.data[0].userImg != null) {

            $("#bigimg").attr("src", res.data[0].userImg);
        } else {
            $("#bigimg").attr("src", "/Img/UserImg/moren.png");
        }
        if (res.data[0].nickName != null) {
            nickName = res.data[0].nickName;
        }
        $("#Nickname").text(nickName);//左上角昵称h2
        console.log(res.data[0].userEmail);
        $("#MyuserEmail").text(res.data[0].userEmail);//邮箱
        console.log($("#MyuserEmail").text());
        $("#nickName").val(nickName);//昵称
        $("#userAge").val(res.data[0].userAge);//年龄
        $("#userSex").val(res.data[0].userSex);//性别
        console.log("原来" + $("#userSex").val());
        $("#userQQQ").val(res.data[0].userQQ);//QQ
        $("#userPhone").val(res.data[0].userPhone);//手机号
    });
}
//修改个人信息按钮点击后更新到数据库
function userbtn() {
    $("#userbtn").click(function () {
        var url = location.search;
        console.log(url);
        var id = parseInt(url.substr(4));
        console.log(id);
        console.log(typeof (id));
        var nickName = $("#nickName").val();
        var userAge = parseInt($("#userAge").val());
        var userSex = parseInt($("#userSex").val());
        console.log("现在"+userSex);
        var userQQ = $("#userQQQ").val();
        var userPhone = $("#userPhone").val();
        console.log(nickName + "类型：" + typeof (nickName));
        console.log(userAge + "类型：" + typeof (userAge));
        console.log(userSex + "类型：" + typeof (userSex));
        console.log(userPhone + "类型：" + typeof (userPhone));
        console.log(userQQ + "类型：" + typeof (userQQ));
        $.post('/Personalinfo/UpdateUserInfo', {
            nickName: nickName,
            userAge: userAge,
            userSex: userSex,
            userQQ: userQQ,
            userPhone: userPhone
        }, function (res) {
            console.log(res);
            if (res.success) {
                window.location.href = "/Jumppage/Jumppage?id=" + id +"?page=/Personalinfo/Personalinfo"+"?msg=1";
            }
        });
    });
}