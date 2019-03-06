$(function () {
    Healinfo();
});
function Healinfo() {
    //发布按钮点击事件
    $("#btnRelease").click(function () {
        var heal_title = $("#txtTitle").val();
        var heal_content = $("#editor").html();
        var heal_type = $("#heal_type").val();
        healtype();
        console.log("接下来为标题内容和类型");
        console.log(heal_title);
        console.log(heal_content);
        console.log(heal_type);
        $.post('/PublishHeal/Sethealth', {
            heal_title: heal_title,
            heal_content: heal_content,
            heal_typeID: heal_type,
            heal_count: 3,
            heal_status: 1
        }, function (res) {
                if (res.success) {
                    alert(res.msg);
                    console.log("成功写入");
                }
                else {
                    alert(res.msg);
                    console.log("写入失败");
                }
            });

        return false;
    });
};
function healtype() {
    var heal_type = $("#heal_type").val();
    if (heal_type == '0') {
        $(".redstart").text("*文章类型必填");
    } else {
        $(".redstart").text("*");
    }
};