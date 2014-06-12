function addCuponToCart(location) {
    $.post(location)
        .done(function (data) {
            if(data != null)
            $('.navbar-right li:nth-child(2) a').html(data);      
        }).fail(function () {
        });
}

$(document).ready(function () {
    $("#subscribe-btn").click(function (e) {
        e.preventDefault();
        $.get("/Ad/Subscribe?id=" + $(".pull-top").attr("id"), function (data) {
        });
    });
});
