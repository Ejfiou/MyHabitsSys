$(function () {
    var mycolor = sessionStorage.getItem('color');
    console.log(mycolor);
    document.body.style.setProperty('--main-color', mycolor);
    clearinp();
    color();
    navbar();
    loginbtnopen();
    console.log(111);

    $('#imgWaiting').hide();


    //检查是否登录
    var logID = $('#logID').val();
    var logImg = $('#logImg').val();
    var logStatus = $('#logStatus').val();
    console.log(logStatus);
    console.log(typeof(logStatus));
    if (logID != "") {
        $(".loginpage").hide();
        $("#mask").css("display", "none");//遮罩隐藏
        $("#nav-log").addClass("disp");
        $("#user_pt").removeClass("disp");
        var href = $("#personbtn").attr("href");
        $("#personbtn").attr("href", href + "?id=" + logID);
        var href1 = $("#personbtn1").attr("href");
        $("#personbtn1").attr("href", href1 + "?id=" + logID);
        console.log($("#personbtn").attr("href"));
       
        if (logStatus == 1) {
            $("#user_admin").removeClass("disp");
            $("#user_pt").addClass("disp");
        } else {
            $("#user_pt").removeClass("disp");
            $("#user_admin").addClass("disp");
        }
    } else {
        $("#nav-log").removeClass("disp");
    }
    if (logImg != null) {
        $(".head_rightimg").attr("src", logImg);
    } else {
        $(".head_rightimg").attr("src", "/img/UserImg/moren.png");
    }
    //用户名文本框失去焦点时
    $('#loginName').blur(function () {
        var userName = $("#loginName").val();
        $.post('/Account/UserIMg', {
            userName: userName,
        }, function (res) {
            if (res.success) {
                if (res.msg != null) {
                    $("#loginimg").attr("src", res.msg);
                } else {
                    $("#loginimg").attr("src", "/Img/UserImg/moren.png");
                }
            }
            else {
                $("#loginimg").attr("src", "/Img/UserImg/moren.png");
            }
        });
        return false;
    });

    $('#UserName').on('change', function () {
        $.ajax({
            url: '/WebService1.asmx/IsLoginIdRepeat',
            type: 'POST',//get
            async: true,//是否异步提交
            data: 'txtUserName=' + $('#UserName').val(),
            dataType: 'text', //返回的数据格式:json/xml/html/script/text
            beforeSend: function (data) {
                $('#imgWaiting').show();
            },
            success: function (data) {
                if (data === 'True') {
                    $('#result').html('*由字母、数字、_组成，非数字开头,4~17位');
                    $('#resulttrue').css("display", "inline");
                    $('#btnReg').attr('disabled', false);
                    
                }
                else {
                    $('#result').html('用户名已存在');
                    $('#btnReg').attr('disabled', true);
                    
                }
            },
            complete: function () {
                $('#imgWaiting').hide();
            }
        });
    });

    //登录按钮点击
    $("#btnLogin").click(function () {
        var userName = $("#loginName").val();
        var userPwd = $("#loginPwd").val();

        $.post('/Account/UserLogin', {
            userName: userName,
            password: userPwd,
        }, function (res) {
            if (res.success) {
                //登录成功
                //alert(res.msg);
                $(".loginpage").hide();
                $("#mask").css("display", "none");//遮罩隐藏
                $("#nav-log").addClass("disp");
                if (res.data[0].userStatus == 1) {
                    $("#user_admin").removeClass("disp");
                    $("#user_pt").addClass("disp");
                } else {
                    $("#user_pt").removeClass("disp");
                    $("#user_admin").addClass("disp");
                }
                console.log(res.data[0]);
                var href = $("#personbtn").attr("href");
                $("#personbtn").attr("href", href + "?id=" + res.data[0].ID);
                console.log($("#personbtn").attr("href"));
                var href1 = $("#personbtn1").attr("href");
                $("#personbtn1").attr("href", href1 + "?id=" + res.data[0].ID);
                if (res.data[0].userImg != null) {
                    $(".head_rightimg").attr("src", res.data[0].userImg);
                } else {
                    $(".head_rightimg").attr("src", "/img/UserImg/moren.png");
                }
            }
            else {
                //登录失败
                $(".spanhidd").css("visibility", "visible");
                //alert(res.msg);
            }
        });

        return false;

    });
    //注册按钮点击
    $("#btnRegist").click(function () {
        var UserName = $("#UserName").val();
        var password = $("#password1").val();
        var userEmail = $("#userEmail").val();
        var userQQ = $("#userQQ").val();
        var emailCode = $("#Vcode").val();
        $.post('/Account/Regist', {
            userName: UserName,
            password: password,
            userAge: 1,
            userSex: 0,
            userEmail: userEmail,
            userStatus: 0,
            userQQ: userQQ,
            userPhone:0,
            emailCode: emailCode,
            
        }, function (res) {
            if (res.success) {
                $("#Vcode").nextAll("span:first").css("display", "inline");
                $("#Vcode").nextAll("span:last").css("display", "none");
                location.reload();
            }
            else {
                $("#Vcode").nextAll("span:first").css("display", "none");
                $("#Vcode").nextAll("span:last").css("display", "inline");
            }
        });

        return false;
    });

    $(".quitbtn").click(function () {
        $.post('/Account/Userquit', {
        }, function (res) {
            console.log(res.msg);
            location.reload(true);
        });
    });
    
    
});
//清空登录注册input框的内容
function clearinp() {
    $("#UserName").val("");
    $("#password1").val("");
    $("#password2").val("");
    $("#userQQ").val("");
    $("#userEmail").val("");
    $("#Vcode").val("");
    $("#loginName").val("");
    $("#loginPwd").val("");
    $("#loginimg").attr("src", "/Img/UserImg/moren.png");
    $(".spantrue").css("display", "none");
    $(".spanfalse").css("display", "none");
    $('#imgWaiting').hide();
}



//导航栏
function navbar() {
    var url;
    var type;
    url = location.search;
    console.log(url);
    type = parseInt(url.substr(6));
    console.log(type);
    console.log(typeof (type));
    $('#navbar .nav ul li').mouseover(function () {
        var con = $(this).index();
        console.log(con);
        $('.bottom-nav').css({
            left: 76 * con + 'px'
        });
    });
   
    if (type == 2) {
        $('.bottom-nav').css({
            left: '152px'
        });
    } else if (type == 4) {
        $('.bottom-nav').css({
            left: '304px'
        });
    } else if (type == 3) {
        $('.bottom-nav').css({
            left: '456px'
        });
    } else {
        $('.bottom-nav').css({
            left: '0px'
        });
    }
    $('#navbar .nav ul li').mouseleave(function () {
        if (type == 2) {
            $('.bottom-nav').css({
                left: '152px'
            });
        } else if (type == 4) {
            $('.bottom-nav').css({
                left: '304px'
            });
        } else if (type == 3) {
            $('.bottom-nav').css({
                left: '456px'
            });
        } else {
            $('.bottom-nav').css({
                left: '0px'
            });
        }
    });
    $('.imgul-tx').hover(function () {
        console.log(111);
        $('.img-arrow').css("transform", "rotate(180deg)");
        $('.drop-down').stop().slideDown();

    }, function () {
        $('.img-arrow').css("transform", "rotate(0deg)");
        $('.drop-down').stop().slideUp();
    });
};
//登陆注册功能入口
function loginbtnopen() {
    $("<div id='mask'></div>").appendTo("body");
    $loginpage = $(".loginpage");//登录页面
    $registerpage = $(".registerpage");//注册页面
    $mask = $("#mask");//遮罩
    var $loginbtn = $("#loginbtn");//登陆按钮
    var $registerbtn = $("#registerbtn");//注册按钮
    $loginpage.hide();//登陆框隐藏
    $registerpage.hide();//注册框隐藏
    $mask.css("display", "none");//遮罩隐藏
    //切换登陆注册页面
    switchpage();
    //点击登陆按钮
    $loginbtn.click(function () { loginbtn() })
    // 点击注册按钮
    $registerbtn.click(function () { registerbtn() })
}
//注册按钮点击事件
function registerbtn() {
    var code = $("#VerificationCode");
    code.unbind("onkeyup", function(){
        console.log('已解除');
});
    var $btnRegist = $("#btnRegist");
    $btnRegist.addClass("graybtndis");
    $btnRegist.attr("disabled", "disabled");
        $mask.css("display", "block");//遮罩显示
        $registerpage.show();
        $loginpage.hide();
        $registerpage.stop()
            .animate({ marginTop: "-200px" }, 1000)
            .fadeIn("slow");
        closelogin();
        FormValidation();
        verif();
}
    //登陆按钮点击效果
    function loginbtn() {
        $mask.css("display", "block");//遮罩显示
        $loginpage.show();//登陆框显示
        $loginpage.stop()
            .animate({ marginTop: "-200px" }, 1000)
            .fadeIn("slow");
        $registerpage.hide();//注册框隐藏
        closelogin();
        FormValidation();
        
    }
    //点击登陆注册框右上角的X关闭窗口
    function closelogin() {
        var $closelogin = $(".closelogin");//右上方X按钮
        $closelogin.click(function () {
            clearinp();
            console.log('1111');
            $(this).parent().hide();
            $mask.css("display", "none");//遮罩隐藏
            $(this).parent().css("marginTop", "-250px");//登陆框回到原来的位置
        });
    };
function FormValidation() {
    var $Inp = $(".registerpage input");
    var code = $("#VerificationCode");
    for (var i = 0; i < $Inp.length-1; i++) {
        $Inp[i].onfocus = function () {
            console.log($("#password1").nextAll("span:first").text());
            if ($(this).attr('id') === "password2" && $("#password1").nextAll("span:first").css("display") === "none") {
                alert('第一次密码输入不正确');
                $Inp[1].focus();
                return;
            }
            fn.call(this, this.id);
        };
        $Inp[i].onkeyup = function () {
            fn.call(this, this.id);
        };
    }
    $("#Vcode").focus(function () {
        btnRegistshow();
    });
    function fn(key) {
        var val = $(this).val();
        console.log($(this).nextAll("span:first").text());
        console.log($(this).nextAll("span:last").text());
        console.log(val);
        console.log(key);
        var bool = key === 'password2' ? val !== $("#password1").val() : !reg[key].test(val);
        if (bool) {
            $('#result').html('*由字母、数字、_组成，非数字开头,4~17位');
            console.log($(this).next('span[class="spantrue"]').text());
            $(this).nextAll("span:first").css("display", "none");
            $(this).nextAll("span:last").css("display", "inline");
        } else {
            console.log($(this).next(".spantrue"));
            $(this).nextAll("span:last").css("display", "none");
            $(this).nextAll("span:first").css("display", "inline");
        }
    };

    //正则集合
    var reg = {
        'UserName': /^[a-zA-Z_]\w{4,17}$/,
        'password1': /^[\w\`\!\@\#\$\%\^&\*\(\)\.\,\+\-\<\>\\\|\/\:\;\"\"\'\'\~\?\[\]\{\}]{4,18}$/,
        'userQQ': /^[1-9]\d{4,9}$/,
        'userEmail': /^\w+@[a-z0-9]{2,}(\.[a-z]{2,}){1,3}$/i,

    };
    };
function switchpage() {
    var $loginSkip = $("#loginSkip");//橙色立即注册
    var $registerSkip = $("#registerSkip");//橙色立即登陆
    // if($(".loginpage").css("display")!="none"){
    $loginSkip.click(function () {
        clearinp();
        $loginpage.css("marginTop", "-250px");//登陆框回到原来的位置
        registerbtn();
    });
    // }else if($(".registerpage").css("display")!="none"){
    $registerSkip.click(function () {
        clearinp();
        $registerpage.css("marginTop", "-250px");//登陆框回到原来的位置
        loginbtn();
    });
};

//注册按钮变灰事件
function btnRegistshow() {
    var $SpanTrue = $(".registerpage .spantrue");
    var index = 0;
    var $btnRegist = $("#btnRegist");
    console.log("jinlaile ");
    for (var i = 0; i < $SpanTrue.length - 1; i++) {
        console.log($SpanTrue[i].style.display);
        console.log(typeof ($SpanTrue[i]));
        if ($SpanTrue[i].style.display ==="inline") {
            index++;
        }
        
    }
    if (index === 5) {
        $btnRegist.removeClass("graybtndis");
        $btnRegist.attr("disabled", false);
    } else {

        $btnRegist.addClass("graybtndis");
        $btnRegist.attr("disabled", "disabled");
    }
    
}
//主体颜色换肤
function color() {
    sessionStorage.setItem('key', 'value');

    // 从sessionStorage获取数据
    
    var color;
    $("#red").click(function (color) {
        console.log(111);
        color = '#ca0c16';
        sessionStorage.setItem('color', color);
        document.body.style.setProperty('--main-color', '#ca0c16');
    });
    $("#green").click(function (color) {
        color = '#1AAD19';
        sessionStorage.setItem('color', color);
        document.body.style.setProperty('--main-color', '#1AAD19');
    });
    $("#blue").click(function (color) {
        color = '#2672ff';
        sessionStorage.setItem('color', color);
        document.body.style.setProperty('--main-color', '#2672ff');
    });
    console.log(color);
   
}


function verif() {
    $("#VerificationCode").click(function (){
        var code = $("#VerificationCode");
        //code.unbind()
        if ($("#userEmail").nextAll("span:first").css("display") === "none") {
            alert("先填写邮箱");
            $("#userEmail").focus();
            return false;
        }
        code.attr("disabled", "disabled");
        var time = 60;
        var set = setInterval(function () {
            code.addClass("graybtndis");
            code.text("(" + --time + ")秒后重新获取");
            if (time == 0) {
                code.removeClass("graybtndis");
                code.attr("disabled", false).text("重新获取验证码");
                clearInterval(set);
            }
        }, 1000);
        var userEmail = $("#userEmail").val();

        $.post('/Account/GetEmailCode', {
            emailAddress: userEmail
        }, function () {

        });

        return false;
    })
}

