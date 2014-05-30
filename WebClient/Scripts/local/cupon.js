function addCuponToCart(location) {
    $.post(location)
        .done(function (data) {
            if (data === "True");
                alert("Added to cart");
        }).fail(function () {
            alert("ERROR: Adding to cart failed.")
        });
}

$(document).ready(function () {
    $("#subscribe-btn").click(function () {
        $.post("../Ad/SubscribeAd", function (data) {
            alert(data);
        });
    });
});