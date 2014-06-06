function addCuponToCart(location) {
    $.post(location)
        .done(function (data) {
            if (data === "True") {
                alert("Added to cart");
            } else {
                alert("ERROR: This is not a valid cupon anymore and may be expired.")
            }
                
        }).fail(function () {
            alert("ERROR: Could not contact API, please check your internet connection.")
        });
}

$(document).ready(function () {
    $("#subscribe-btn").click(function () {
        $.post("../Ad/SubscribeAd", function (data) {
            alert(data);
        });
    });
});
