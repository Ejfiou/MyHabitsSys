$(function () {
    iCheck();
    doload();
});
function doload() {
    Getquestioninfo();
   // echart();
    console.log(12333343434);
    //$($('input')[0]).iCheck('check');
   
}

function iCheck() {
    $('input').iCheck({
        labelHover: true,
        cursor: true,
        checkboxClass: 'icheckbox_flat-red',
        radioClass: 'iradio_flat-red'
    });
}


function Getquestioninfo() {
    var url = location.search;
    console.log(url);
    var id = parseInt(url.substr(8));
    console.log(id);
    console.log(typeof (id));
    $.post('/QuestionInfo/GetQuesInfo', {
        questionID: id
    }, function (res) {
        console.log(res);
        if (res.success) {
            console.log("发布成功");

            var question_centent = JSON.parse(res.data[0].question_centent);
            console.log(question_centent);
            $(".title_content").text(res.data[0].question_title);

            var idnum = 0;
            var namenum = 0;
            var $question_option;
            for (var i = 0; i < question_centent.length; i++) {
                var $question_centent = $(
                    '<div class="question_centent ">\n' +
                    '<div class="Q_title"><span class="q_seq">1</span><span class="q_title">标题</span></div>\n' +
                    '<div class="echartbtn">\n' +
                    '<div class="tabtn Piechart">饼状图</div> <div class="tabtn Histogram">柱状图</div> <div class="tabtn Circularchart">圆环图</div><br/>\n' +
                    '</div >\n' +
                    '</div >');
                //'<div class= "echartmain" > </div >\n' +
                namenum++;
                $("#Ques_centent").append($question_centent);
                $(".question_centent:last").children().children(".q_seq").text("Q" + (i + 1));
                $(".question_centent:last").children().children(".q_title").text(question_centent[i].title);
                if (question_centent[i].type == 1) {
                    for (var j = 0; j < question_centent[i].option.length; j++) {
                        $($(".question_centent")[i]).addClass("q_radio");
                        $question_option = $(
                            '  <div class="q_centent_wrap">\n' +
                            ' <div class= "iradio mybtnimg">\n' +
                            ' <input type="radio" name="quux[1]">\n' +
                            ' </div> <label  class="option_title">Bar</label> <br />\n' +

                            '</div >');
                        idnum++;
                        $(".question_centent:last").find(".echartbtn").before($question_option);
                        $(".question_centent:last").children(".q_centent_wrap:last").find("input").attr({ "id": "baz" + idnum, "name": "quux" + namenum, "value": j });

                        $(".question_centent:last").children(".q_centent_wrap:last").find(".option_title").attr("for", "baz" + idnum);
                        $(".question_centent:last").children(".q_centent_wrap:last").find(".option_title").text(question_centent[i].option[j].valoption);
                    }
                } else if (question_centent[i].type == 2) {
                    $($(".question_centent")[i]).addClass("q_check");
                    for (var j = 0; j < question_centent[i].option.length; j++) {

                        $question_option = $(
                            '  <div class="q_centent_wrap">\n' +
                            ' <div class= "icheckbox mybtnimg">\n' +
                            ' <input type="checkbox" name="quux[1]">\n' +
                            ' </div> <label  class="option_title">Bar</label> <br />\n' +
                            '</div >');
                        idnum++;
                        $(".question_centent:last").find(".echartbtn").before($question_option);
                        $(".question_centent:last").children(".q_centent_wrap:last").find("input").attr({ "id": "baz" + idnum, "name": "quux" + namenum, "value": j });
                        $(".question_centent:last").children(".q_centent_wrap:last").find(".option_title").attr("for", "baz" + idnum);
                        $(".question_centent:last").children(".q_centent_wrap:last").find(".option_title").text(question_centent[i].option[j].valoption);
                    }
                } else if (question_centent[i].type == 3) {

                    $($(".question_centent")[i]).addClass("q_textval");

                    $question_option = $(
                        '  <div class="q_centent_wrap">\n' +
                        '<div class="Q_text" contenteditable="flase"></div>\n' +
                        '</div >');
                    $(".question_centent:last").find(".echartbtn").remove();
                    $(".question_centent:last").find(".echartmain").remove();
                    $(".question_centent:last").append($question_option);
                }
            }
            $('input').iCheck('disable');
            MyAnswerinfo(id);
            AllAnswerinfo(id,question_centent);
            iCheck();
        } else {
            console.log("发布失败");
        }
    });
}


function MyAnswerinfo(id) {
    var logID = $('#logID').val();//userID
    console.log(logID);
    $.post('/Answerinfo/MyAnswerinfo', {
        questionID: id,
        userID: logID
    }, function (res) {
        console.log(res);
        if (res.success) {
            console.log("取自己成功");
            var answer_centent = JSON.parse(res.data[0].answer_centent);
            console.log(answer_centent[0].type);
            console.log(typeof (answer_centent[0].type));
            console.log(answer_centent);
            var aval;
            var question_centent = $(".question_centent");
            for (var i = 0; i < question_centent.length; i++) {
                if (answer_centent[i].type== 1) {
                     aval = parseInt(answer_centent[i].aval);
                    $(question_centent[i]).find("input:eq(" + aval + ")").iCheck('check');
                    $($(question_centent[i]).find("label:eq(" + aval + ")")).addClass("colfu");
                    console.log($(question_centent[i]).find("label:eq(" + aval + ")"));
                } else if (answer_centent[i].type == 2) {
                    console.log("复选框");
                    console.log(answer_centent[i].aval);
                    if (answer_centent[i].aval.length > 1) {
                        var avalarr = answer_centent[i].aval;
                        for (var j = 0; j < avalarr.length; j++) {
                            aval = parseInt(avalarr[j]);
                            $(question_centent[i]).find("input:eq(" + aval + ")").iCheck('check');
                            $($(question_centent[i]).find("label:eq(" + aval + ")")).addClass("colfu");
                        }
                    } else {
                        console.log(answer_centent[i].aval[0]);
                        aval = parseInt(answer_centent[i].aval[0]);
                        $(question_centent[i]).find("input:eq(" + aval + ")").iCheck('check');
                        $($(question_centent[i]).find("label:eq(" + aval + ")")).addClass("colfu");
                    }
                } else if (answer_centent[i].type == 3) {
                    $(question_centent[i]).find(".Q_text").text(answer_centent[i].aval);
                }
                
            }
        } else {
            console.log("取值失败");
        }
        });
}
function AllAnswerinfo(id, question_centent) {
    console.log(question_centent);
    $.post('/Answerinfo/AllAnswerinfo', {
        questionID: id,
    }, function (res) {
        console.log(res);
        if (res.success) {
            console.log(question_centent);
            console.log("取所有成功");
            var chartdata = [];
            var title;
            var type;
            var aval;
            var option;
            var alldate = [];
            for (var i = 0; i < res.data.length; i++) {
                var len = [];
                var valoption = "";
                var num = 0;
                var count;
                var answer_centent = JSON.parse(res.data[i].answer_centent);
                console.log(answer_centent);
                for (var k = 0; k < answer_centent.length; k++) {

                    if (answer_centent[k].type == 1) {
                        title = question_centent[k].title;
                        type = question_centent[k].type;
                        for (var j = 0; j < question_centent[k].option.length; j++) {
                            count = 0;
                            aval = parseInt(answer_centent[k].aval);
                            console.log(aval);
                            if (aval == j) {
                                count++;
                                console.log(count);
                            }
                            option = {
                                valoption: question_centent[k].option[j].valoption,
                                count: count,
                                num: k,
                                type: question_centent[k].type
                            }
                            len.push(option);

                        }
                    } else if (answer_centent[k].type == 2) {
                        title = question_centent[k].title;
                        type = question_centent[k].type;
                        for (var j = 0; j < question_centent[k].option.length; j++) {
                            count = 0;
                            console.log(answer_centent[k].aval);
                            for (var x = 0; x < answer_centent[k].aval.length; x++) {
                                aval = parseInt(answer_centent[k].aval[x]);
                                if (aval == j) {
                                    count++;
                                    console.log(count);
                                }
                            }
                            option = {
                                valoption: question_centent[k].option[j].valoption,
                                count: count,
                                num: k,
                                type: question_centent[k].type
                            }
                            len.push(option);

                        }
                    } else if (answer_centent[k].type == 3) {
                        option = {
                            num: k,
                            type: question_centent[k].type
                        }
                        len.push(option);
                    }
                }
                    var centent = {
                        annum: i,
                        option: len,
                    }
                    chartdata.push(centent);
                    if (i > 0) {
                        for (var z = 0; z < chartdata[i].option.length; z++) {
                            if (chartdata[i - 1].option[z].valoption == chartdata[i].option[z].valoption) {
                                if (chartdata[i - 1].option[z].type != 3) {
                                    chartdata[i].option[z].count += chartdata[i - 1].option[z].count;
                                }
                            }
                        }

                    }
                    console.log(chartdata);
                
            }
            
           // console.log(chartdata[chartdata.length - 1]);
            var sumdata = chartdata[chartdata.length - 1];
            console.log(sumdata);
            Allclickbtn(sumdata);
        } else {
            chartdata.log("取值失败");
        }
    });
    }
function Allclickbtn(sumdata) {
    var Histogram = $(".Histogram");
    var Piechart = $(".Piechart");
    var Circularchart = $(".Circularchart");
    var echartmain;
    for (var i = 0; i < Histogram.length; i++) {
        $(Histogram[i]).click(function () {
            var bool = $(this).parent().parent().find(".hisecharts");
            console.log(bool);
            if (bool.length !=0) {
                console.log($(this));
                $(this).parent().parent().find(".hisecharts").remove();
            } else {
                if ($(this).parent().parent().find(".echartmain").length != 0) {
                    $(this).parent().parent().find(".echartmain").remove();
                }
                echartmain = '<div class="echartmain hisecharts" ></div>';
                $(this).parent().after(echartmain);
                console.log($(this).closest(".question_centent").index());
                var index = $(this).closest(".question_centent").index();
                var newvaloption = [];
                var newcount = [];
                for (var j = 0; j < sumdata.option.length; j++) {
                    if (sumdata.option[j].num == index){
                        newvaloption.push(sumdata.option[j].valoption);
                        newcount.push(sumdata.option[j].count);
                    }
                }
                console.log(newvaloption);
                console.log(newcount);
                var annum = sumdata.annum + 1;
                HisEchart(index, newvaloption, newcount,annum);
            }
        });

        $(Piechart[i]).click(function () {
            var bool = $(this).parent().parent().find(".pieecharts");
            console.log(bool);
            if (bool.length != 0) {
                console.log($(this));
                $(this).parent().parent().find(".pieecharts").remove();
            } else {
                if ($(this).parent().parent().find(".echartmain").length !=0) {
                    $(this).parent().parent().find(".echartmain").remove();
                }
                var echartmain = '<div class="echartmain pieecharts" ></div>';
                $(this).parent().after(echartmain);
                console.log($(this).closest(".question_centent").index());
                var index = $(this).closest(".question_centent").index();
                var echardata = [];
                for (var j = 0; j < sumdata.option.length; j++) {
                    if (sumdata.option[j].num == index) {
                        var option = {
                            value: sumdata.option[j].count ,
                            name: sumdata.option[j].valoption
                        }
                        echardata.push(option);
                    }
                }
                console.log(echardata);
                var annum = sumdata.annum + 1;
                PieEchart(index, echardata, annum);
            }
        });

        $(Circularchart[i]).click(function () {
            var bool = $(this).parent().parent().find(".cirecharts");
            console.log(bool);
            if (bool.length != 0) {
                console.log($(this));
                $(this).parent().parent().find(".cirecharts").remove();
            } else {
                if ($(this).parent().parent().find(".echartmain").length != 0) {
                    $(this).parent().parent().find(".echartmain").remove();
                }
                var echartmain = '<div class="echartmain cirecharts" ></div>';
                $(this).parent().after(echartmain);
                console.log($(this).closest(".question_centent").index());
                var index = $(this).closest(".question_centent").index();
                var echardata = [];
                for (var j = 0; j < sumdata.option.length; j++) {
                    if (sumdata.option[j].num == index) {
                        var option = {
                            value: sumdata.option[j].count,
                            name: sumdata.option[j].valoption
                        }
                        echardata.push(option);
                    }
                }
                console.log(echardata);
                var annum = sumdata.annum + 1;
                CirEchart(index, echardata, annum);
            }
        });




    };
};

function HisEchart(index, newvaloption, newcount, annum) {
    var main = $($(".question_centent")[index]).find(".hisecharts");
    var myChart = echarts.init(main.get(0));
    // 柱形图
    var option = {
        title: {
            text: '本题有效填写人次: ' + annum+'人'
        },
        tooltip: {},
        xAxis: {
            data: newvaloption,
            //formatter: function (data) {
            //    console.log(params);
            //    var strs = data.split(''); //字符串数组
            //    var str = ''
            //    for (var i = 0, s; s = strs[i++];) { //遍历字符串数组
            //        str += s;
            //        if (!(i % 2)) str += '\n'; //按需要求余
            //    }
            //    return str
            //}
        },
        yAxis: {},
        axisLabel: {
            interval: 0,
            formatter: function (value) {
                var result = "";//拼接加\n返回的类目项
                var maxLength = 5;//每项显示文字个数
                var valLength = value.length;//X轴类目项的文字个数
                var rowNumber = Math.ceil(valLength / maxLength); //类目项需要换行的行数
                if (rowNumber > 1)//如果文字大于3,
                {
                    for (var i = 0; i < rowNumber; i++) {
                        var temp = "";//每次截取的字符串
                        var start = i * maxLength;//开始截取的位置
                        var end = start + maxLength;//结束截取的位置
                        temp = value.substring(start, end) + "\n";
                        result += temp; //拼接生成最终的字符串
                    }
                    return result;
                }
                else {
                    return value;
                }
            }
        },
        series: [{
            name: '',
            type: 'bar',
            data: newcount,
            itemStyle: {
                normal: {
                    color: function (params) {
                        var colorList = ['#74c5f7', '#f4b760', '#ee7c6c', '#73d76e', '#549bd3', '#f47e39'];
                        return colorList[params.dataIndex];
                    }
                },
            },
            label: {
                normal: {
                    show: true,            //显示数字
                    position: 'top',        //这里可以自己选择位置
                    fontSize: '20',
                    formatter: function (params) {
                        var value = parseInt(params.value) / annum;
                        var bai = (value * 100).toFixed(2) + '%';
                        return bai;
                    },
                }
            }
        }]
    };
    // 使用刚指定的配置项和数据显示图表。
    myChart.setOption(option);
};

function PieEchart(index, echardata, annum) {
    var main = $($(".question_centent")[index]).find(".pieecharts");
    var myChart = echarts.init(main.get(0));
    myChart.setOption({
        title: {
            text: '本题有效填写人次: ' + annum + '人'
        },
        tooltip: {
            trigger: 'item',
            formatter:"{a} <br/>{b} : {c}"
        },

        series: [
            {
                name: '访问来源',
                type: 'pie',
                radius: '55%',
                data: echardata,
                itemStyle: {
                    normal: {
                        label: {
                            show: true,
                            formatter: function (e) {
                                var newStr = " ";
                                var start, end;
                                var name_len = e.name.length;    　　　　　　　　　　　　   //每个内容名称的长度
                                var max_name = 7;    　　　　　　　　　　　　　　　　　　//每行最多显示的字数
                                var new_row = Math.ceil(name_len / max_name); 　　　　// 最多能显示几行，向上取整比如2.1就是3行
                                if (name_len > max_name) { 　　　　　　　　　　　　　　  //如果长度大于每行最多显示的字数
                                    for (var i = 0; i < new_row; i++) { 　　　　　　　　　　　   //循环次数就是行数
                                        var old = '';    　　　　　　　　　　　　　　　　    //每次截取的字符
                                        start = i * max_name;    　　　　　　　　　　     //截取的起点
                                        end = start + max_name;    　　　　　　　　　  //截取的终点
                                        if (i == new_row - 1) {    　　　　　　　　　　　　   //最后一行就不换行了
                                            old = e.name.substring(start);
                                        } else {
                                            old = e.name.substring(start, end) + "\n";
                                        }
                                        newStr += old; //拼接字符串
                                    }
                                } else {                                          //如果小于每行最多显示的字数就返回原来的字符串
                                    newStr = e.name;
                                }
                                var value = parseInt(e.value) / annum;
                                var bai = (value * 100).toFixed(2) + '%';
                                var allstr = newStr + ":\n" + bai;
                                return allstr;
                            },
                                //'{b}:\n ({d}%)',
                            fontSize: '15',
                        },
                        color: function (params) {
                            var colorList = ['#74c5f7', '#f4b760', '#ee7c6c', '#73d76e', '#549bd3', '#f47e39'];
                            return colorList[params.dataIndex];
                        }
                    },
                },
                labelLine: { show: true }
            }
        ]
    })
};

function CirEchart(index, echardata, annum) {
    var main = $($(".question_centent")[index]).find(".cirecharts");
    var myChart = echarts.init(main.get(0));
    myChart.setOption({
        title: {
            text: '本题有效填写人次: ' + annum + '人'
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b} : {c}"
        },
        series: [
            {
                name: '访问来源',
                type: 'pie',
                radius: ['30%', '55%'],
                data: echardata,
                itemStyle: {
                    normal: {
                        label: {
                            show: true,
                            fontSize: '15',
                            formatter: function (e) {
                                var newStr = " ";
                                var start, end;
                                var name_len = e.name.length;    　　　　　　　　　　　　   //每个内容名称的长度
                                var max_name = 7;    　　　　　　　　　　　　　　　　　　//每行最多显示的字数
                                var new_row = Math.ceil(name_len / max_name); 　　　　// 最多能显示几行，向上取整比如2.1就是3行
                                if (name_len > max_name) { 　　　　　　　　　　　　　　  //如果长度大于每行最多显示的字数
                                    for (var i = 0; i < new_row; i++) { 　　　　　　　　　　　   //循环次数就是行数
                                        var old = '';    　　　　　　　　　　　　　　　　    //每次截取的字符
                                        start = i * max_name;    　　　　　　　　　　     //截取的起点
                                        end = start + max_name;    　　　　　　　　　  //截取的终点
                                        if (i == new_row - 1) {    　　　　　　　　　　　　   //最后一行就不换行了
                                            old = e.name.substring(start);
                                        } else {
                                            old = e.name.substring(start, end) + "\n";
                                        }
                                        newStr += old; //拼接字符串
                                    }
                                } else {                                          //如果小于每行最多显示的字数就返回原来的字符串
                                    newStr = e.name;
                                }
                                var value = parseInt(e.value) / annum;
                                var bai = (value * 100).toFixed(2) + '%';
                                var allstr = newStr + ":\n" + bai;
                                return allstr;
                            },
                        },
                        color: function (params) {
                            var colorList = ['#74c5f7', '#f4b760', '#ee7c6c', '#73d76e', '#549bd3', '#f47e39'];
                            return colorList[params.dataIndex];
                        }
                    },
                },
                labelLine: { show: true }
            }
        ]
    })
};

function echart() {
    console.log("进来了");
    // 基于准备好的dom，初始化echarts实例
    var main = $("#main");
    var myChart = echarts.init(main.get(0));

    //// 柱形图
    //var option = {
    //    title: {
    //        text: 'ECharts 入门示例'
    //    },
    //    tooltip: {},
    //    legend: {
    //        data: ['销量']
    //    },
    //    xAxis: {
    //        data: ["衬衫", "羊毛衫", "雪纺衫", "裤子", "高跟鞋", "袜子"]
    //    },
    //    yAxis: {},
    //    series: [{
    //        name: '销量',
    //        type: 'bar',
    //        data: [5, 20, 36, 10, 10, 20]
    //    }]
    //};
    //// 使用刚指定的配置项和数据显示图表。
    //myChart.setOption(option);

    //饼状图
    myChart.setOption({
        series: [
            {
                name: '访问来源',
                type: 'pie',
                radius: '55%',
                data: [
                    { value: 235, name: '视频广告' },
                    { value: 274, name: '联盟广告' },
                    { value: 310, name: '邮件营销' },
                    { value: 335, name: '直接访问' },
                    { value: 400, name: '搜索引擎' }
                ]
            }
        ]
    })

    ////环形图
    //myChart.setOption({
    //    series: [
    //        {
    //            name: '访问来源',
    //            type: 'pie',
    //            radius: ['39%', '55%'],
    //            data: [
    //                { value: 235, name: '视频广告' },
    //                { value: 274, name: '联盟广告' },
    //                { value: 310, name: '邮件营销' },
    //                { value: 335, name: '直接访问' },
    //                { value: 400, name: '搜索引擎' }
    //            ]
    //        }
    //    ]
    //})

}


//for (var i = 0; i < res.data.length; i++) {
//    var len = [];

//    var answer_centent = JSON.parse(res.data[i].answer_centent);
//    console.log(answer_centent);
//    if (answer_centent[i].type == 1) {
//        title = question_centent[i].title;
//        type = question_centent[i].type;
//        for (var j = 0; j < question_centent[i].option.length; j++) {

//            aval = parseInt(answer_centent[i].aval);
//            console.log(aval);
//            if (aval == j) {
//                count++;
//                console.log(count);
//            }
//            option = {
//                valoption: question_centent[i].option[j].valoption,
//                count: count,
//                num: j
//            }
//            len.push(option);
//        }
//    }
//    var centent = {
//        title: title,
//        option: len,
//        type: type
//    }
//    chartdata.push(centent);
//}


//console.log(chartdata);