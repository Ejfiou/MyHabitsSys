$(function () {
    count();
});


/*
 *msg为1时  为  个人信息
 *msg为2时 为  个人密码
 *msg为3时 为  个人头像
 * */
function count() {
    var url = location.search;
    console.log(url);
    var info = url.split("?");
    console.log(info);
    var id = parseInt(info[1].substr(3));
    console.log(id);
    var page = info[2].substr(5);
    console.log(page);
    var msgnum = info[3].substr(4);
    console.log(msgnum);
    var msg;
    if (msgnum == '1') {
        msg = "个人信息";
    } else if (msgnum == '2') {
        msg = "个人密码";
    } else if (msgnum == '3') {
        msg = "个人头像";
    }
    timeID = 0;
    timeLeft = 3;
    console.log(msg);
    $('#msg').text(msg);
    setInterval(function () {
        timeLeft--;
        $('#Timemin').text(timeLeft);
        if (timeLeft <= 0) {
            clearInterval(timeID);
            $('#Timemin').text('0');
            location.href = page+'?id='+id;
        }
        }, 1000);
}
