$(document).ready(function () {
    $("#subscribe-btn").click(function (e) {
        e.preventDefault();
        $.get("/Institution/Subscribe?id=" + $(".pull-top").attr("id"), function (data) {
        });
    });
});