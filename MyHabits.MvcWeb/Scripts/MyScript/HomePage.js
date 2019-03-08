$(function () {
    doLoad();
    leftText();
});
function leftText() {
    console.log("进来了");
    var $leftullen = $(".heal-left ul li").length;
    //var $leftText = $($(".heal-left ul li")[0]).text();
    //console.log($leftText);
    console.log($leftullen);
    for (var i = 0; i < $leftullen; i++) {
        var $leftText = $($(".heal-left ul li")[i]).text();
        console.log($leftText);
    }
}
function doLoad() {
    $.post('/PublishHeal/GetHealthInfo', function (data) {
        console.log(data.data.length);
        var $leftullen = $(".heal-left ul li").length;
        //console.log(data[0]);
        //console.log(data.heal_title);
        for (var i = 0; i < $leftullen; i++) {
            var $leftText = $($(".heal-left ul li")[i]);
            var $lefta = $($("#leftul a")[i]);
            console.log($lefta.attr('href'));
            var heal_title = data.data[i].heal_title;
            $leftText.text(heal_title);
        }
        });
    return false;
}