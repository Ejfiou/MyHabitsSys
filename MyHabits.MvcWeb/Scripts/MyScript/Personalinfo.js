$(function () {
    doload();
});

function doload() {
    MyUserInfo();
    userbtn();
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
   
}
function MyUserInfo() {
    var url = location.search;
    console.log(url);
    var id = parseInt(url.substr(4));
    console.log(id);
    console.log(typeof (id));
    $.post('/Personalinfo/SetMyuserInfo', {
        ID: id,
    }, function (res) {
        console.log(res);
        if (res.data[0].userImg != null) {

            $("#bigimg").attr("src", res.data[0].userImg);
        } else {
            $("#bigimg").attr("src", "~/Img/UserImg/moren.png");
        }
        $("#userEmail").text(res.data[0].userEmail);//邮箱
        $("#nickName").val(res.data[0].nickName);//昵称
        $("#userAge").val(res.data[0].userAge);//年龄
        $("#userSex").val(res.data[0].userSex);//性别
        $("#userQQQ").val(res.data[0].userQQ);//QQ
        $("#userPhone").val(res.data[0].userPhone);//手机号
    });
}

function userbtn() {
    $("#userbtn").click(function () {
        var userEmail = $("#userEmail").text();
        var nickName = $("#nickName").val();
        var userAge = parseInt($("#userAge").val());
        var userSex = $("#userSex").val();
        var userQQ = $("#userQQQ").val();
        var userPhone = $("#userPhone").val();
        console.log(userEmail + "类型：" + typeof (userEmail));
        console.log(nickName + "类型：" + typeof (nickName));
        console.log(userAge + "类型：" + typeof (userAge));
        console.log(userSex + "类型：" + typeof (userSex));
        console.log(userPhone + "类型：" + typeof (userPhone));
        console.log(userQQ + "类型：" + typeof (userQQ));
        
    });
}