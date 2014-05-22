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

        $.ajax({
            url: "/Ad/Delete",
            type: "POST",
            data: { id: id },
            success: function () {
                alert("deleted");
                ad.remove();
            }
        });
    }

});
