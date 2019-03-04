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