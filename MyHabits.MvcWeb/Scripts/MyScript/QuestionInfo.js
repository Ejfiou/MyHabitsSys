$(function () {
    iCheck();
    doload();
});
function doload() {
    Getquestioninfo();
}
function iCheck() {
    $('input').iCheck({
        checkboxClass: 'icheckbox_flat-red',
        radioClass: 'iradio_flat-red'
    });
}
function Getquestioninfo() {
    $.post('/QuestionInfo/GetQuesInfo', {
        questionID:2
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
            //for (var i = 0; i < question_centent.length; i++) {
            //    var $question_centent = $(
            //        '<div class="question_centent ">\n' +
            //        '<div class="Q_title"><span class="q_seq">1</span><span class="q_title">标题</span></div>\n' +
            //        '</div >');
            //    $("#Ques_centent").append($question_centent);
            //    $(".question_centent:last").children().children(".q_seq").text("Q" + (i + 1));
            //    $(".question_centent:last").children().children(".q_title").text(question_centent[i].title);
            //    if (question_centent[i].type == 1) {
            //        console.log(question_centent[i].title);
            //        for (var j = 0; j < question_centent[i].option.length; j++) {
            //            console.log("you");
            //            var $question_option = $(
            //                ' <div class="q_centent_wrap">选项一</div>');
            //            $(".question_centent:last").append($question_option);
            //            $(".question_centent:last").children(".q_centent_wrap:last").text(question_centent[i].option[j].valoption);
            //        }
            //    }
            //}
        } else {
            console.log("发布失败");
        }
    });
}