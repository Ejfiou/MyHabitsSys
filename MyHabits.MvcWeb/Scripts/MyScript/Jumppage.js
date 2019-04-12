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
        msg = "个人信息已修改";
    } else if (msgnum == '2') {
        msg = "个人密码已修改";
    } else if (msgnum == '3') {
        msg = "个人头像已修改";
    } else if (msgnum == '4') {
        msg = "问卷已发布";
    } else if (msgnum == '5') {
        msg = "健康资讯已发布";
    } else if (msgnum == '6') {
        msg = "健康资讯已保存到草稿箱";
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
            if (msgnum == '1' || msgnum == '2' || msgnum == '3') {
                location.href = page + '?id=' + id;
            } else if (msgnum == '4' || msgnum == '5' || msgnum == '6') {
                //window.location.go(-1); //刷新上一页，不可行看下面
                //window.history.go(-1);
                self.location = document.referrer;
                
            }
            
        }
        }, 1000);
}
