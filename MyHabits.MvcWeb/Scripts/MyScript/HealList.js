$(function () {

    doload();
    
});
function doload() {
    getheallist();
    typeID();
    Notop();
}
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
function typeID() {
    var url = location.search;
    console.log(url);
    var type = parseInt(url.substr(6));
    console.log(type);
    console.log(typeof (type));
    if (type == 2) {
        $("#healtypeID").text("健康资讯");
    } else if (type == 4) {
        $("#healtypeID").text("美容护肤");
    } else if (type == 3) {
        $("#healtypeID").text("健康养生");
    }
}
function getheallist() {
    var url = location.search;
    console.log(url);
    var type = parseInt(url.substr(6));
    console.log(type);
    console.log(typeof (type));
    var mycount = 0;
    $.post('/HealList/GetHealList', {
        heal_typeID: type,
        pageSize: 10,
        page: 1
    }, function (res) {
        if (res.success) {
            mycount = res.count

        } else {
            console.log("失败");
        }
        });


    //重复执行某个方法 
    var t1 = window.setInterval(function myfunction() {

        if (mycount > 0) {
            window.clearInterval(t1); 
            layui.use(['laypage', 'layer'], function () {
                var laypage = layui.laypage
                    , layer = layui.layer;
                laypage.render({
                    elem: 'paging'
                    , count: mycount
                    , layout: ['count', 'prev', 'page', 'next', 'limit', 'refresh', 'skip']
                    , jump: function (obj) {
                        var url = location.search;
                        console.log(url);
                        var type = parseInt(url.substr(6));
                        console.log(type);
                        console.log(typeof (type));
                        $.post('/HealList/GetHealList', {
                            heal_typeID: type,
                            pageSize: obj.limit,
                            page: obj.curr
                        }, function (res) {
                            if (res.success) {
                                
                                console.log(res);
                                //这里删除以前的li
                                $("#healtypelist").find("li").remove();
                                for (var i = 0; i < res.data.length; i++) {
                                    var healli = '<li><a href=""><span class="healtitle"></span><span class="sd_time"></span></a></li>'
                                    $("#healtypelist").append(healli);
                                    $("#healtypelist li").last().find(".healtitle").text(res.data[i].heal_title);
                                    var dt = res.data[i].heal_sdTime;
                                    var formatTime1 = convertTime(dt, "MM月dd日 hh:mm");//2019年03月16日 20:46 47秒
                                    //mydatetime(formatTime1);
                                    $("#healtypelist li").last().find(".sd_time").text(formatTime1);
                                    $("#healtypelist li").last().find("a").attr("href", "/HomepageInfo/HomepageInfo?id="+res.data[i].ID);
                                }
                            } else {
                                console.log("失败");
                            }
                        });

                    }
                });
            });
        }
    }, 300);
    

    

   
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