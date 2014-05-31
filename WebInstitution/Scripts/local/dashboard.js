$(document).ready(function () {
	
});

function initialize() {
     var map_canvas = document.getElementById('map_canvas');
    var map_options = {
      center: new google.maps.LatLng(44.5403, -78.5463),
      zoom: 8,
      mapTypeId: google.maps.MapTypeId.ROADMAP
    }
    var map = new google.maps.Map(map_canvas, map_options)
}


$(document).on("click","a[data-ajax='true']", function(e){
    e.preventDefault();
    $($(this).attr("data-ajax-update")).load(this.href, function() {
    	initialize();
    });
    
});

$(document).on("click", "#deleteAd", function (e) {
    e.preventDefault();

    if (confirm("Are you sure you want do delete this Ad?")) {
        var ad = $(this).closest(".cupon-wrapper");
        var id = ad.attr("data-id");

        $.post(
            '/Ad/Delete',
            { id: id },
            function (data) {
                $("#partial").html(data);
            });
    }

});

$(document).on("click", "#saveSettings", function (e) {
    e.preventDefault();

    var formData = $(this).closest("form").serializeArray();

    $.post(
        '/Account/SubmitDetails',
        formData,
        function (data) {
            $("#partial").html(data);
        });

});

$(document).on("click", "#createAd", function (e) {
    e.preventDefault();
    alert("here");

    $(this).closest("form").submit();

   /* var formData = $(this).closest("form").serializeArray();

    $.post(
        '/Ad/Create',
        formData,
        function (data) {
            $("#partial").html(data);
        });*/

});

$(document).on("click", "#importAd", function (e) {
    e.preventDefault();

    var adForm = $("#newAdForm");
    var template = $(this).closest("li").find(".details");

    var price = template.find("#ad_price").val();
    var discount = template.find("#ad_discount").val();

    var prevPrice = -1 * (100 * price / (discount - 100));

    adForm.find("#service").val(template.find("#ad_service").val());
    adForm.find("#specialty").val(template.find("#ad_specialty").val());
    adForm.find("#name").val(template.find("#ad_name").val());
    adForm.find("#price").val(price);
    adForm.find("#previous_price").val(Math.round(prevPrice));
    adForm.find("#description").val(template.find("#ad_description").val());
});
