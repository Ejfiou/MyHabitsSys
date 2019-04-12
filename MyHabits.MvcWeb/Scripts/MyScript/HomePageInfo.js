$(function () {
    doLoad();
    //leftText();
});
function doLoad() {
    gethealinfo();
    Notop();
};

function Notop() {
    var rightul = $("#rightul");
    var rightli = rightul.children("li");
    $.post('/HealList/GetHealNotop', {
    }, function (res) {
        if (res.success) {
            console.log(res);
            console.log("成功");
            console.log(rightli.length);
            for (var i = 0; i < rightli.length; i++) {
                $(rightli[i]).find(".healrig_title").text(res.data[i].heal_title);
                $(rightli[i]).find("a").attr("href", "/HomepageInfo/HomepageInfo?id=" + res.data[i].ID);
            }
        } else {
            console.log("失败");
        }
    });
}

function gethealinfo() {
    console.log("进来了");
    var url = location.search;
    console.log(url);
    var id = parseInt(url.substr(4));
    console.log(id);
    console.log(typeof (id));
    $.post('/PublishHeal/SetHealthInfo', {
        ID: id,
    }, function (res) {
        if (res.success) {
            //成功
            console.log("成功");
            console.log(res);
            $("#HealTitle").text(res.data[0].heal_title);
            $("#HealContent").html(res.data[0].heal_content);
            var dt = res.data[0].heal_sdTime;
            var formatTime1 = convertTime(dt, "yyyy年MM月dd日 hh:mm ss秒");//2019年03月16日 20:46 47秒
            console.log(formatTime1.substr(0, 17));
            var sdtiome = formatTime1.substr(0, 17);//发布时间
            $("#HealContent").find("p").attr("contenteditable", false);
            $("#HealContent").find("div").attr("contenteditable", false);
            $("#sdtime").text("发布时间：" + sdtiome); 
            $("#count").text("阅读次数：" + res.data[0].heal_count);
        }
        else {

            alert(res.msg);
        }
    });

    return false;
};




//json时间转换
function convertTime(jsonTime, format) {
    var date = new Date(parseInt(jsonTime.replace("/Date(", "").replace(")/", ""), 10));
    var formatDate = date.format(format);
    return formatDate;
}
Date.prototype.format = function (format) {
    var date = {
        "M+": this.getMonth() + 1,
        "d+": this.getDate(),
        "h+": this.getHours(),
        "m+": this.getMinutes(),
        "s+": this.getSeconds(),
        "q+": Math.floor((this.getMonth() + 3) / 3),
        "S+": this.getMilliseconds()
    };

    if (/(y+)/i.test(format)) {
        format = format.replace(RegExp.$1, (this.getFullYear() + '').substr(4 - RegExp.$1.length));
    }

    for (var k in date) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? date[k] : ("00" + date[k]).substr(("" + date[k]).length));
        }
    }

    return format;
}
