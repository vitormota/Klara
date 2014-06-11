$(document).ready(function () {
    $("#subscribe-btn").click(function (e) {
        e.preventDefault();
        $.get("/Institution/" +$(".pull-top").attr("id") + "/Subscribe", function (data) {
        });
    });
});