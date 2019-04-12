$(function () {
    doload();
});
function doload() {
 //   getquestion();
    Getheal2();

}


function healFstatus(res) {
    var logStatus = $('#logStatus').val();
    console.log(logStatus);
    if (logStatus == 1) {
        var $li = $("#listul").children("li");
        console.log($li);
        for (var i = 0; i < $li.length; i++) {
            $($li[i]).hover(function () {
                $(this).find(".delimg").removeClass("disp");
                $(this).find(".delimg").click(function () {
                    healID = $(this).closest("li").attr("id");
                    console.log($(this).closest("li").attr("id"));
                    $.post('/PublishHeal/UpdateHealthInfo2', {
                        ID: healID
                    }, function (res) {
                        if (res.success) {
                            console.log("转换类型成功");
                        } else {
                            console.log("失败");
                        }
                    });
                });
            }, function () {
                $(this).find(".delimg").addClass("disp");
            });
            //$($li).find(".delimg").removeClass("disp");
            //$($li[i]).find(".delimg").hover(function () {
            //    $(this).children("img").attr("src", "../Img/delimg.png");
            //}, function () {
            //    $(this).children("img").attr("src", "../Img/delgray.png");
            //});
        }
    }
}



function Getheal2() {

    $.post('/PublishHeal/GetHealthInfo2', {
    }, function (res) {
        console.log(res);
        if (res.success) {
            console.log(res.data.length);
            for (var i = 0; i < res.data.length; i++) {
                $("#listul").append('<li><div class="icon">资讯</div><div class="q_title"></div><br /><span class="edit_time"></span><div class="delimg disp">发布</div></li>');
                console.log($("#listul").children("li:last-child"));
                var lastli = $("#listul").children("li:last-child");
                lastli.attr("id", res.data[i].ID);
                lastli.find(".q_title").text(res.data[i].heal_title);
                var dt = res.data[0].heal_sdTime;
                var formatTime1 = convertTime(dt, "MM月dd日 hh:mm");//2019年03月16日 20:46 47秒
                //mydatetime(formatTime1);
                lastli.find(".edit_time").text(formatTime1);
            //    lastli.find("a").attr("href", "/QuestionInfo/QuestionInfo?" + res.data[i].questionID)
            }
            healFstatus(res);
        } else {
            console.log("失败");
        }
    });
}
function mydatetime(formatTime1) {
    var myDate = new Date();
    var Year = myDate.getFullYear();
    var month = myDate.getMonth();       //获取当前月份(0-11,0代表1月)
    var day = myDate.getDate();        //获取当前日(1-31)
    var hours = myDate.getHours();       //获取当前小时数(0-23)
    var min = myDate.getMinutes();     //获取当前分钟数(0-59)
    console.log(formatTime1);
    var mytime = formatTime1.split("/");
    console.log(Year + '/' + month + '/' + day + '/' + hours + '/' + min + '/');

}

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