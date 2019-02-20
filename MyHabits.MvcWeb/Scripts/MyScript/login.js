$(function () {
    loginbtnopen();
    console.log(111);
})
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
};
//注册按钮点击事件
function registerbtn() {
    $mask.css("display", "block");//遮罩显示
    $registerpage.show();
    $loginpage.hide();
    $registerpage.stop()
        .animate({ marginTop: "-200px" }, 1000)
        .fadeIn("slow");
    closelogin();
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
}
//点击登陆注册框右上角的X关闭窗口
function closelogin() {
    var $closelogin = $(".closelogin");//右上方X按钮
    $closelogin.click(function () {
        console.log('1111');
        $(this).parent().hide();
        $mask.css("display", "none");//遮罩隐藏
        $(this).parent().css("marginTop", "-250px");//登陆框回到原来的位置
    })
};
function switchpage() {
    var $loginSkip = $("#loginSkip");//橙色立即注册
    var $registerSkip = $("#registerSkip");//橙色立即登陆
    // if($(".loginpage").css("display")!="none"){
    $loginSkip.click(function () {
        $loginpage.css("marginTop", "-250px");//登陆框回到原来的位置
        registerbtn();
    });
    // }else if($(".registerpage").css("display")!="none"){
    $registerSkip.click(function () {
        $registerpage.css("marginTop", "-250px");//登陆框回到原来的位置
        loginbtn();
    });

    $("#btnRegist").click(function () {
        var UserName = $("#UserName").val();
        var password = $("#password1").val();
        var userEmail = $("#userEmail").val();
        var userQQ = $("#userQQ").val();
        var emailCode = $("#Vcode").val();
        $.post('/Account/Regist', {

            userName: UserName,
            password: password,
            userEmail: userEmail,
            userQQ: userQQ,
            emailCode: emailCode
        }, function (res) {
            if (res.success) {
                alert(res.msg)
            }
            else {
                alert(res.msg)
            }
            });

        return false;
    });

    $("#VerificationCode").click(function () {
        var userEmail = $("#userEmail").val();
      
        $.post('/Account/GetEmailCode', {
            emailAddress: userEmail           
        }, function () {
           
        });

        return false;
    });
    // }
}