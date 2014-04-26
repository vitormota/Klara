$(document).ready(function () {   
    var badge1 = false;

    $("#div-badge1").click(function () {
        if (!badge1) {
            $("#dp-badge1").removeClass("glyphicon-chevron-right").addClass("glyphicon-chevron-down");
            badge1 = true;
        } else {
            $("#dp-badge1").removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-right");
            badge1 = false;
        }
    });

    var badge2 = false;
    $("#div-badge2").click(function () {
        if (!badge2) {
            $("#dp-badge2").removeClass("glyphicon-chevron-right").addClass("glyphicon-chevron-down");
            badge2 = true;
        } else {
            $("#dp-badge2").removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-right");
            badge2 = false;
        }
    });

    var badge3 = false;
    $("#div-badge3").click(function () {
        if (!badge3) {
            $("#dp-badge3").removeClass("glyphicon-chevron-right").addClass("glyphicon-chevron-down");
            badge3 = true;
        } else {
            $("#dp-badge3").removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-right");
            badge3 = false;
        }
    });
});