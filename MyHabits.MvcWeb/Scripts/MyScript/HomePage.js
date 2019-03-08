$(function () {
    doLoad();
    //leftText();
});

function doLoad() {
    $.post('/PublishHeal/GetHealthInfo', function (data) {
        var $leftul = $(".heal-left ul li");
        var $lefta = $(".leftula");
        $lefta.each(function (i) {
            var href = $(this).attr("href");
            var heal_title = data.data[i].heal_title;
            $(this).text(heal_title);
            $(this).attr("href", href+"?id="+ data.data[i].ID);
            console.log($(this).attr("href"));
        });
    });
    
}