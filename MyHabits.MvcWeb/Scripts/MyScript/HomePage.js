$(function () {
    doLoad();
    //leftText();
});

function doLoad() {
    RotationChart();
    GetHealthInfo();
}
function GetHealthInfo() {
    $.post('/PublishHeal/GetHealthInfo', function (data) {
        var $leftul = $(".heal-left ul li");
        var $lefta = $(".leftula");
        $lefta.each(function (i) {
            var href = $(this).attr("href");
            var heal_title = data.data[i].heal_title;
            $(this).text(heal_title);
            $(this).attr("href", href + "?id=" + data.data[i].ID);
            console.log($(this).attr("href"));
        });
    });
};
function RotationChart() {
    $.post('/PublishHeal/GetHealRotation', {
    }, function (res) {
        if (res.success) {
            console.log(res);
            console.log("成功");
            var $cententli = $(".poster-list").children("li");
            $cententli.each(function (i) {
                $(this).find(".poster-item-title").text(res.data[i].heal_title);
                $(this).find("a").attr("href", "/HomepageInfo/HomepageInfo?id=" + res.data[i].ID);
            });
        } else {
            console.log("失败");
        }
        //var $leftul = $(".heal-left ul li");
        //var $lefta = $(".leftula");
        //$lefta.each(function (i) {
        //    var href = $(this).attr("href");
        //    var heal_title = data.data[i].heal_title;
        //    $(this).text(heal_title);
        //    $(this).attr("href", href + "?id=" + data.data[i].ID);
        //    console.log($(this).attr("href"));
        //});
    });
}