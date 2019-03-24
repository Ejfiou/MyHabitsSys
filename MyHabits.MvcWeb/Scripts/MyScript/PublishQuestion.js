$(function () {
    doload();
});
function doload() {
    subbtn();
    nulltip();
    radiobtn();//单选按钮点击事件
    checkbtn();//多选按钮点击事件
    textbtn();// 文本按钮点击事件
    sexbtn();// 性别按钮点击事件
    tipsbtn();// 备注按钮点击事件
    cententclick();
    cententhover();//模块选中时事件
    optionfoucus();//选项focus和hover事件
    //addoptionfocus();//添加选项按钮事件
    cententindex();
  
}
function nulltip() {
    var $question_centent = $(".question_centent");
    console.log($question_centent.length);
    if ($question_centent.length == 0) {
        $(".question_null_tip").removeClass("disp"); 
    } else{
        $(".question_null_tip").addClass("disp");
    }
}
function cententindex() {
    var $question_centent = $(".question_centent");
    var $Q_option_value = $(".Q_option_value");
    for (var i = 0; i < $question_centent.length; i++) {
        var optionnum = $($question_centent[i]).children(".q_centent_wrap").length
        $($question_centent[i]).find(".q_seq").text(i+1);
        console.log($($question_centent[i]).find(".q_seq").text(i + 1));
        
    };
}
function deloption(){
    var deloption = $(".deloption");
   // console.log(deloption);
    for (var i = 0; i < deloption.length; i++) {
        $(deloption[i]).click(function () {
            $(this).closest(".q_centent_wrap").remove();
        });
        
    }
    //optinonindex();
}
function addoptionfocus() {
    console.log("进来啦");
    var addoption = $(".addoption");
   // console.log(addoption);
    for (var i = 0; i < addoption.length;i++) {
        $(addoption[i]).hover(function () {
            $(this).css("background", "#f4f4f4")
        }, function () {
            $(this).css("background", "#fff");
            });
        $(addoption[i]).click(function () {
            var option1;
            var radioimglen = $(this).parent().prev().children().children().children(".radio_img").length;
            console.log($(this).parent().prev().children().children().children(".radio_img").length);
            if (radioimglen > 0) {
                 option1 = '<div class="q_centent_wrap">\n' +
                    '<div class="q_optionval">\n' +
                    '<div class="radio_centent_wrap Q_option">\n' +
                    '<div class="radio_img" contenteditable="false"></div>\n' +
                    '<div class="ques_centent Q_option_value" contenteditable="true"></div>\n' +
                    '</div> \n' +
                    '<img src="../Img/delziimg.png" class="disp deloption" />\n' +
                    ' </div>\n' +
                    '</div>\n';
            } else{
                option1 = '<div class="q_centent_wrap">\n' +
                    '<div class="q_optionval">\n' +
                    '<div class="radio_centent_wrap Q_option">\n' +
                    '<div class="check_img" contenteditable="false"></div>\n' +
                    '<div class="ques_centent Q_option_value" contenteditable="true"></div>\n' +
                    '</div> \n' +
                    '<img src="../Img/delziimg.png" class="disp deloption" />\n' +
                    ' </div>\n' +
                    '</div>\n';
            }
            $(this).parent().prev().after(option1);
         //   console.log($(this).parent().parent().children('.q_centent_wrap').length);
            var optionindex = $(this).parent().parent().children('.q_centent_wrap').length;
            $(this).parent().prev().find(".Q_option_value").text('选项' + optionindex);
            optionfoucus();
            deloption();
        });
    }
        
}
function optionfoucus() {
    //选项获得焦点时背景变黑
    var $Q_option_value = $(".Q_option_value");
    //   console.log($Q_option_value);
    var $q_optionval = $(".q_optionval");
    for (var i = 0; i < $Q_option_value.length; i++) {
        $($Q_option_value[i]).focus(function () {
            // console.log($(this).parent());
            $(this).parent().css("border", "1px solid transparent")
                .css("background", "rgba(128, 133, 144, .15)").addClass("myfocus");
            $(this).parent().next("img").removeClass("disp");
            //$(this).parents(".question_centent").addClass("question_focus");
        });
        $($Q_option_value[i]).blur(function () {
            $(this).parent().css("border", "1px solid transparent")
                .css("background", "#fff").removeClass("myfocus");
            //   console.log($(this).next().children("img"));
            $(this).parent().next("img").addClass("disp");
        });
        //选项hover时虚线
        $($q_optionval[i]).hover(function () {
            if ($(this).children(".Q_option").hasClass("myfocus")) {
                return false;
            } else {
                $(this).children(".Q_option").css("border", "1px dashed #808080");
                $(this).children("img").removeClass("disp");
            }
        }, function () {
            $(this).children(".Q_option").css("border", "1px solid transparent");
            if ($(this).children(".Q_option").hasClass("myfocus")) {
                return false;
            } else {
                $(this).children("img").addClass("disp");
            }
        });
    }
}

function cententhover() {
    var $question_centent = $(".question_centent");
    var $aaa = $(".aaa");
    console.log($question_centent);
    for (var i = 0; i < $question_centent.length; i++) {
        //$($question_centent[i]).focus(function () {
        //    $(this).addClass("question_focus");
        //    $(this).children(".q_title_wrap").children("img").removeClass("disp");
        //});
        //$($question_centent[i]).blur(function () {
        //    $(this).removeClass("question_focus");
        //    $(this).children(".q_title_wrap").children("img").addClass("disp");
        //});
        $($question_centent[i]).hover(function () {
            if ($(this).hasClass("question_focus")) {
                return false;
            } else {
                $(this).children(".q_title_wrap").children("img").removeClass("disp");
            }
        }, function () {
            if ($(this).hasClass("question_focus")) {
                return false;
            } else {
                $(this).children(".q_title_wrap").children("img").addClass("disp");
            }
        });
    }

}

function cententclick() {
    $(document).bind("click", function (e) {
        if ($(e.target).closest(".question_centent").length > 0) {
         //   console.log($(e.target).closest(".question_centent").index());
            var $centent = $(e.target).closest(".question_centent");
            $centent.addClass("question_focus");
            $centent.children(".q_title_wrap").children("img").removeClass("disp");
            $centent.children(".addoption_centent").removeClass("disp");
            $('.question_centent').not($centent).removeClass("question_focus");
            $('.question_centent').not($centent).children(".q_title_wrap").children("img").addClass("disp");
            $('.question_centent').not($centent).children(".addoption_centent").addClass("disp");
            var cententimg = $centent.children(".q_title_wrap").children("img");
            cententimg.click(function () {
                $centent.remove();
                nulltip();
                cententindex();
            })
        } else {
          //  console.log("没有");
            $(".question_centent").removeClass("question_focus");
            $(".question_centent").children(".q_title_wrap").children("img").addClass("disp");
            $(".question_centent").children(".addoption_centent").addClass("disp");
        }
    }); 
}
function radiobtn() {
    $("#radiobtn").click(function () {
        var index = $(".question_centent").length + 1;
        //$("#footend").before(radioinfo1);
        var radioinfo1 = $(
            '<div class="question_centent q_radio_val" tabindex="0">\n' +
            '<div class="q_title_wrap ">\n' +
            '<div class="q_seq">1</div>\n' +
            '<div class="radio_title Dottedline" contenteditable="true"><p class="Q_title">单选题</p></div>\n' +
            '<img src="../Img/delimg.png" class="disp" />\n' +
            '</div >\n' +
            '<div class="q_centent_wrap">\n' +
            '<div class="q_optionval">\n' +
            '<div class="radio_centent_wrap Q_option">\n' +
            '<div class="radio_img" contenteditable="false"></div>\n' +
            '<div class="ques_centent Q_option_value" contenteditable="true">选项1</div>\n' +
            '</div> \n' +
            '<img src="../Img/delziimg.png" class="disp deloption" />\n' +
            ' </div>\n' +
            '</div >\n' +
            '<div class="q_centent_wrap ">\n' +
            '<div class="q_optionval">\n' +
            '<div class="radio_centent_wrap Q_option">\n' +
            '<div class="radio_img" contenteditable="false"></div>\n' +
            '<div class="ques_centent Q_option_value" contenteditable="true">选项2</div>\n' +
            '</div >\n' +
            '<img src="../Img/delziimg.png" class="disp deloption" />\n' +
            '</div >\n' +
            '</div>\n' +
            '<div class="addoption_centent disp"><div class="addoption"><img src="../Img/addoption.png" />添加单个选项</div></div>\n' +
            '</div>');
        $("#footend").before(radioinfo1);
        //$("#footend").prev().text();
        //$("#footend").prev().find(".q_seq").text(index);
        //console.log($("#footend").prev().find(".q_seq").text(index));
        cententindex();
        nulltip();
        addoptionfocus();
        deloption();
        cententclick();
        cententhover();//模块选中时事件
        optionfoucus();//选项focus和hover事件
        
        
        
    });
}
function checkbtn() {
    $("#checkbtn").click(function () {
        var index = $(".question_centent").length + 1;
        //$("#footend").before(radioinfo1);
        var checkinfo1 = $(
            '<div class="question_centent q_check_val" tabindex="0">\n' +
            '<div class="q_title_wrap ">\n' +
            '<div class="q_seq">1</div>\n' +
            '<div class="radio_title Dottedline" contenteditable="true"><p class="Q_title">多选题</p></div>\n' +
            '<img src="../Img/delimg.png" class="disp" />\n' +
            '</div >\n' +
            '<div class="q_centent_wrap">\n' +
            '<div class="q_optionval">\n' +
            '<div class="radio_centent_wrap Q_option">\n' +
            '<div class="check_img" contenteditable="false"></div>\n' +
            '<div class="ques_centent Q_option_value" contenteditable="true">选项1</div>\n' +
            '</div> \n' +
            '<img src="../Img/delziimg.png" class="disp deloption" />\n' +
            ' </div>\n' +
            '</div >\n' +
            '<div class="q_centent_wrap ">\n' +
            '<div class="q_optionval">\n' +
            '<div class="radio_centent_wrap Q_option">\n' +
            '<div class="check_img" contenteditable="false"></div>\n' +
            '<div class="ques_centent Q_option_value" contenteditable="true">选项2</div>\n' +
            '</div >\n' +
            '<img src="../Img/delziimg.png" class="disp deloption" />\n' +
            '</div >\n' +
            '</div>\n' +
            '<div class="addoption_centent disp"><div class="addoption"><img src="../Img/addoption.png" />添加单个选项</div></div>\n' +
            '</div>');
        $("#footend").before(checkinfo1);
        //$("#footend").prev().text();
        //$("#footend").prev().find(".q_seq").text(index);
        //console.log($("#footend").prev().find(".q_seq").text(index));
        cententindex();
        nulltip();
        addoptionfocus();
        deloption();
        cententclick();
        cententhover();//模块选中时事件
        optionfoucus();//选项focus和hover事件


    });
}

function textbtn() {
    $("#textbtn").click(function () {//填空题
        var textbtn = 1;
        setcentent(textbtn);
    });
    $("#namebtn").click(function () {//姓名
        var namebtn = 2;
        setcentent(namebtn);
    });
    $("#phonebtn").click(function () {//手机
        var phonebtn = 3;
        setcentent(phonebtn);
    });
    $("#emailbtn").click(function () {//邮箱
        var emailbtn = 4;
        setcentent(emailbtn);
    });
    $("#agebtn").click(function () {//年龄
        var agebtn = 5;
        setcentent(agebtn);
    });
    $("#qbtn").click(function () {//QQ号
        var qbtn = 6;
        setcentent(qbtn);
    });
}
function setcentent(idbtn) {
    var index = $(".question_centent").length + 1;
    //$("#footend").before(radioinfo1);
    var textinfo1 = $(
        '<div class="question_centent" tabindex="0">\n' +
        '<div class="q_title_wrap ">\n' +
        '<div class="q_seq">1</div>\n' +
        '<div class="radio_title Dottedline" contenteditable="true"><p class="Q_title"></p></div>\n' +
        '<img src="../Img/delimg.png" class="disp" />\n' +
        '</div >\n' +
        '<div class="q_centent_wrap">\n' +
        '<div class="q_optionval">\n' +
        '<div class="q_cententFill"></div>\n' +
        ' </div>\n' +
        '</div>');
    $("#footend").before(textinfo1);
    //$("#footend").prev().text();
    //$("#footend").prev().find(".q_seq").text(index);
    if (idbtn == 1) {
        $("#footend").prev().find(".radio_title").children(".Q_title").text("填空题");
    }
    switch (idbtn) {
        case 1:
            $("#footend").prev().find(".radio_title").children(".Q_title").text("填空题");
            break;
        case 2:
            $("#footend").prev().find(".radio_title").children(".Q_title").text("姓名");
            break;
        case 3:
            $("#footend").prev().find(".radio_title").children(".Q_title").text("手机");
            break;
        case 4:
            $("#footend").prev().find(".radio_title").children(".Q_title").text("邮箱");
            break;
        case 5:
            $("#footend").prev().find(".radio_title").children(".Q_title").text("年龄");
            break;
        case 6:
            $("#footend").prev().find(".radio_title").children(".Q_title").text("QQ号");
            break;
    }
    //console.log($("#footend").prev().find(".q_seq").text(index));
    cententindex();
    nulltip();
    //addoptionfocus();
    //deloption();
    cententclick();
    cententhover();//模块选中时事件
        //optionfoucus();//选项focus和hover事件

}
function sexbtn() {
    $("#sexbtn").click(function () {
        var index = $(".question_centent").length + 1;
        //$("#footend").before(radioinfo1);
        var sexinfo1 = $(
            '<div class="question_centent q_radio_val" tabindex="0">\n' +
            '<div class="q_title_wrap ">\n' +
            '<div class="q_seq">1</div>\n' +
            '<div class="radio_title Dottedline" contenteditable="true"><p class="Q_title">性别</p></div>\n' +
            '<img src="../Img/delimg.png" class="disp" />\n' +
            '</div >\n' +
            '<div class="q_centent_wrap">\n' +
            '<div class="q_optionval">\n' +
            '<div class="radio_centent_wrap Q_option">\n' +
            '<div class="radio_img" contenteditable="false"></div>\n' +
            '<div class="ques_centent Q_option_value" >男</div>\n' +
            '</div> \n' +
            ' </div>\n' +
            '</div >\n' +
            '<div class="q_centent_wrap ">\n' +
            '<div class="q_optionval">\n' +
            '<div class="radio_centent_wrap Q_option">\n' +
            '<div class="radio_img" contenteditable="false"></div>\n' +
            '<div class="ques_centent Q_option_value" >女</div>\n' +
            '</div >\n' +
            '</div >\n' +
            '</div>\n' +
            '</div>');
        $("#footend").before(sexinfo1);
        //$("#footend").prev().text();
        $("#footend").prev().find(".q_seq").text(index);
        console.log($("#footend").prev().find(".q_seq").text(index));
        nulltip();
        cententclick();
        cententhover();//模块选中时事件
    });
}

function tipsbtn() {
    $("#tipsbtn").click(function () {
        var index = $(".question_centent").length + 1;
        //$("#footend").before(radioinfo1);
        var tipsinfo1 = $(
            '<div class= "question_centent Q_tips_val" tabindex = "0"> \n' +
            '<div class= "q_title_wrap " > \n' +
            '<div class= "q_seq" > 1</div > \n' +
            '<div class="radio_title Dottedline" contenteditable="true"><p class="Q_cut">分割符号</p></div> \n' +
            '<div class= "q_tips Dottedline" contenteditable = "true"> <p class="Q_title">备注</p></div > \n' +
            '<img src="../Img/delimg.png" class="disp" /> \n' +
            '</div > \n' +
            '</div>');
        $("#footend").before(tipsinfo1);
        //$("#footend").prev().text();
        //$("#footend").prev().find(".q_seq").text(index);
        //console.log($("#footend").prev().find(".q_seq").text(index));
        cententindex();
        nulltip();
        cententclick();
        cententhover();//模块选中时事件


    });
}

function subbtn() {
    console.log("提交啦");
    $("#subbtn").click(function () {
        var $question_centent = $(".question_centent");
        var title_content = $(".title_content");//问卷标题
        var prefix_content = $(".prefix_content");//问卷提醒
        var question = [];
        var title;
        var type;
        for (var i = 0; i < $question_centent.length; i++) {
            console.log($($question_centent[i]).length);
            var len = [];
            if ($($question_centent[i]).hasClass("q_radio_val")) {
                console.log("有radio哦");
                type = 1;
                var Q_option_value = $($question_centent[i]).find(".Q_option_value");
                console.log($($question_centent[i]).find(".Q_option_value"));
                for (var j = 0; j < Q_option_value.length; j++) {
                    var option = {
                        valoption: $(Q_option_value[j]).text()
                    }
                    len.push(option);
                }
            } else if ($($question_centent[i]).hasClass("q_check_val")) {
                console.log("有check哦");
                type = 2;
                var Q_option_value = $($question_centent[i]).find(".Q_option_value");
                console.log($($question_centent[i]).find(".Q_option_value"));
                for (var j = 0; j < Q_option_value.length; j++) {
                    var option = {
                        valoption: $(Q_option_value[j]).text()
                    }
                    len.push(option);
                }
            } else {
                console.log("有文本框哦");
                type = 3;
                len = [];
            }
            if ($($question_centent[i]).hasClass("Q_tips_val")) {
                len = $($question_centent[i]).find(".q_tips").text();
                title = $($question_centent[i]).find(".Q_cut").text();
                type = 4;
            } else {
                title = $($question_centent[i]).find(".radio_title").text();
            }
            console.log(title);
            var centent = {
                title: title,
                option: len,
                type: type
            }
            question.push(centent);
           
        }
        var question_title = $("#question_title").text();
        var question_prefix = $(".prefix_content").text();
        console.log(question);
        console.log(question_title);
        console.log(question_prefix);
        var question_centent = JSON.stringify(question);
        $.post('/PublishQuestion/SetQuesInfo', {
            question_title: question_title,
            question_centent: question,
            question__status: 1,
            question_prefix: question_prefix
        }, function (res) {
            console.log(res);
            if (res.success) {
                console.log("发布成功");
            } else {
                console.log("发布失败");
            }
        });
    })
}