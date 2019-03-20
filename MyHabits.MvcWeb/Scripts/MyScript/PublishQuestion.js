$(function () {
    doload();
});
function doload() {
    
    cententclick();
    cententhover();//模块选中时事件
    optionfoucus();//选项focus和hover事件
    addoptionfocus();//添加选项按钮事件
    radiobtn();//单选按钮点击事件
}
function addoptionfocus() {
    var addoption = $(".addoption");
    console.log(addoption);
    for (var i = 0; i < addoption.length;i++) {
        $(addoption[i]).hover(function () {
            $(this).css("background", "#f4f4f4")
        }, function () {
            $(this).css("background", "#fff");
            });
        $(addoption[i]).click(function () {
            var option1 = '<div class="q_centent_wrap">\n' +
                '<div class="q_optionval">\n' +
                '<div class="radio_centent_wrap Q_option">\n' +
                '<div class="radio_img" contenteditable="false"></div>\n' +
                '<div class="radio_centent Q_option_value" contenteditable="true"></div>\n' +
                '</div> \n' +
                '<img src="../Img/delziimg.png" class="disp" />\n' +
                ' </div>\n' +
                '</div >\n';
            $(this).parent().prev().after(option1);
            console.log($(this).parent().parent().children('.q_centent_wrap').length);
            var optionindex = $(this).parent().parent().children('.q_centent_wrap').length;
            $(this).parent().prev().find(".Q_option_value").text('选项'+optionindex);
        });
    }
        
}
function optionfoucus() {
    //选项获得焦点时背景变黑
    var $Q_option_value = $(".Q_option_value");
    //   console.log($Q_option_value);
    var $Q_option = $(".Q_option");
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
        $($Q_option[i]).hover(function () {
            if ($(this).hasClass("myfocus")) {
                return false;
            } else {
                $(this).css("border", "1px dashed #808080");
                $(this).next("img").removeClass("disp");
            }
        }, function () {
            $(this).css("border", "1px solid transparent");
            if ($(this).hasClass("myfocus")) {
                return false;
            } else {
                $(this).next("img").addClass("disp");
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
        var index = $(".question_centent").length+1;
        //$("#footend").before(radioinfo1);
        var radioinfo1 = $(
        '<div class="question_centent " tabindex="0">\n' +
            '<div class="q_title_wrap ">\n' +
                 '<div class="q_seq">1</div>\n' + 
                 '<div class="radio_title Dottedline" contenteditable="true"><p>单选题</p></div>\n' +
                 '<img src="../Img/delimg.png" class="disp" />\n' +
            '</div >\n' +
            '<div class="q_centent_wrap">\n' +
                '<div class="q_optionval">\n' +
                    '<div class="radio_centent_wrap Q_option">\n' +
                         '<div class="radio_img" contenteditable="false"></div>\n' +
                         '<div class="radio_centent Q_option_value" contenteditable="true">选项1</div>\n' +
                    '</div> \n' +
                    '<img src="../Img/delziimg.png" class="disp" />\n' +
               ' </div>\n' +
            '</div >\n' +
            '<div class="q_centent_wrap ">\n'+
                 '<div class="q_optionval">\n'+
                    '<div class="radio_centent_wrap Q_option">\n'+
                        '<div class="radio_img" contenteditable="false"></div>\n'+
                        '<div class="radio_centent Q_option_value" contenteditable="true">选项2</div>\n'+
                    '</div >\n'+
                    '<img src="../Img/delziimg.png" class="disp" />\n'+
                '</div >\n'+
            '</div>\n' +
            '<div class="addoption_centent disp"><div class="addoption"><img src="../Img/addoption.png" />添加单个选项</div></div>\n'+
           '</div>');
        $("#footend").before(radioinfo1);
        //$("#footend").prev().text();
        $("#footend").prev().find(".q_seq").text(index);
        console.log($("#footend").prev().find(".q_seq").text(index));

        cententclick();
        cententhover();//模块选中时事件
        optionfoucus();//选项focus和hover事件
        addoptionfocus();
    })
}