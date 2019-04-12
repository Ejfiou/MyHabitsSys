$(function () {
    
    doload();
});
function doload() {
    iCheck();
    Getquestioninfo();
    checknow();
}
function iCheck() {
    $('input').iCheck({
        labelHover: true,
        cursor: true, 
        checkboxClass: 'icheckbox_flat-red',
        radioClass: 'iradio_flat-red'
    });
}
function checknow() {
    $("#subbtn").click(function () { 
        tags();
    });
}
function tags() {
    var $question_centent = $(".question_centent");
    var q_centent_wrap;
    var name1;
    var logID = $('#logID').val();//userID
    console.log(logID);
    var url = location.search;
    var quesid = parseInt(url.substr(1));//QuesID
    console.log(quesid);
    var answer_centent = [];
    var aval;
    var type;
    for (var i = 0; i < $question_centent.length; i++) {
        
        if ($($question_centent[i]).hasClass("q_radio")) {
            console.log("有radio");
            var radio = $($question_centent[i]).find("input[type='radio']");
            name1 = $(radio[0]).attr("name");
            console.log(name);
            q_centent_wrap = $($question_centent[i]).find(".q_centent_wrap");
            $("input[name='" + name1 + "']:checked").each(function (index) {
                var radioval = $(this).val();
                console.log(radioval);
                aval = radioval;
                type = 1;
            });
        }
        else if ($($question_centent[i]).hasClass("q_check")) {
            var arrchechbox = [];
            console.log("有check");
            var check = $($question_centent[i]).find("input[type='checkbox']");
            name1 = $(check[0]).attr("name");
            q_centent_wrap = $($question_centent[i]).find(".q_centent_wrap");
            $("input[name='" + name1 + "']:checked").each(function (index) {
                var checkval = $(this).val();
                console.log(checkval);
                arrchechbox.push(checkval);

            }); 
            aval = arrchechbox;
            type = 2;
        } else if ($($question_centent[i]).hasClass("q_textval")) {
            var text = $($question_centent[i]).find(".Q_text").text();
            console.log(text);
            aval = text;
            type = 3;
    }
        var centent = {
            aval: aval,
            type:type
        }
        answer_centent.push(centent);
        console.log(answer_centent);
       
    }
    $.post('/QuestionInfo/SetAnswerinfo', {
        questionID: quesid,
        userID: logID,
        answer_centent: JSON.stringify(answer_centent)
    }, function (res) {
        console.log(res);
        if (res.success) {
            console.log("成功");
            window.location.href = "/Answerinfo/Answerinfo?quesid=" + quesid;
        } else {
            console.log("失败");
        }
    });
}

//function tags() {
//    var $question_centent = $(".question_centent");
//    var q_centent_wrap;
//    var name1;
//    var logID = $('#logID').val();
//    console.log(logID);
//    for (var i = 0; i < $question_centent.length; i++) {

//        if ($($question_centent[i]).hasClass("q_radio")) {
//            console.log("有radio");
//            var radio = $($question_centent[i]).find("input[type='radio']");
//            name1 = $(radio[0]).attr("name");
//            console.log(name);
//            q_centent_wrap = $($question_centent[i]).find(".q_centent_wrap");
//            $("input[name='" + name1 + "']:checked").each(function (index) {
//                var radioval = $(this).val();
//                console.log(radioval);
//            });
//        }
//        else if ($($question_centent[i]).hasClass("q_check")) {
//            var arrchechbox = [];
//            console.log("有check");
//            var check = $($question_centent[i]).find("input[type='checkbox']");
//            name1 = $(check[0]).attr("name");
//            q_centent_wrap = $($question_centent[i]).find(".q_centent_wrap");
//            $("input[name='" + name1 + "']:checked").each(function (index) {
//                var checkval = $(this).val();
//                console.log(checkval);
//                arrchechbox.push(checkval);
//            });
//            console.log(arrchechbox);
//        }
//    }

//}


function Getquestioninfo() {
    var url = location.search;
    console.log(url);
    var id = parseInt(url.substr(1));
    console.log(id);
    console.log(typeof (id));
    $.post('/QuestionInfo/GetQuesInfo', {
        questionID:id
    }, function (res) {
        console.log(res);
        if (res.success) {
            console.log("发布成功");
            console.log(res.data[0].question_centent);
            var question_centent = JSON.parse(res.data[0].question_centent);
            console.log(question_centent);
            $(".title_content").text(res.data[0].question_title);
            $(".prefix_content").text(res.data[0].question_prefix);
            console.log(question_centent.length);
            var idnum = 0;
            var namenum = 0;
            var $question_option;
            for (var i = 0; i < question_centent.length; i++) {
                var $question_centent = $(
                    '<div class="question_centent ">\n' +
                    '<div class="Q_title"><span class="q_seq">1</span><span class="q_title">标题</span></div>\n' +
                    '</div >');
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
                        $(".question_centent:last").append($question_option);
                        $(".question_centent:last").children(".q_centent_wrap:last").find("input").attr({ "id": "baz" + idnum, "name": "quux" + namenum, "value": j });
                        
                        $(".question_centent:last").children(".q_centent_wrap:last").find(".option_title").attr("for", "baz" + idnum);
                        $(".question_centent:last").children(".q_centent_wrap:last").find(".option_title").text(question_centent[i].option[j].valoption);
                        console.log($(".question_centent:last").children(".q_centent_wrap:last").find(".option_title").text());
                        console.log(question_centent[i].option[j].valoption );
                    }
                } else if (question_centent[i].type == 2) {
                    $($(".question_centent")[i]).addClass( "q_check");
                    for (var j = 0; j < question_centent[i].option.length; j++) {
                        console.log("you");
                        $question_option = $(
                            '  <div class="q_centent_wrap">\n' +
                            ' <div class= "icheckbox mybtnimg">\n' +
                            ' <input type="checkbox" name="quux[1]">\n' +
                            ' </div> <label  class="option_title">Bar</label> <br />\n' +
                            '</div >');
                        idnum++;
                        $(".question_centent:last").append($question_option);
                        $(".question_centent:last").children(".q_centent_wrap:last").find("input").attr({ "id": "baz" + idnum, "name": "quux" + namenum, "value": j });
                        $(".question_centent:last").children(".q_centent_wrap:last").find(".option_title").attr("for", "baz" + idnum);
                        $(".question_centent:last").children(".q_centent_wrap:last").find(".option_title").text(question_centent[i].option[j].valoption);
                    }
                } else if (question_centent[i].type == 3){
                    console.log(question_centent[i].title);
                    $($(".question_centent")[i]).addClass("q_textval");
                        console.log("you");
                        $question_option = $(
                            '  <div class="q_centent_wrap">\n' +
                            '<div class="Q_text" contenteditable="true"></div>\n' +
                            '</div >');
                        $(".question_centent:last").append($question_option);
                        }
            }
            iCheck();
        } else {
            console.log("发布失败");
        }
    });
}