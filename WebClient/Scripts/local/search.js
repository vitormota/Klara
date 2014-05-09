$(document).ready(function () {
    var el = $('#search-filters-wrapper');
    elt = el.offset().top;
	$(window).scroll(function() {
		scrolloffset = Math.floor($(this).scrollTop() * 6.250) / 100;
		if(scrolloffset > 8.2) {
			el.css("position", "fixed");
    		el.css("top", 0);
   		 } else {
   		 	el.css("position", "absolute");
   		 	el.css("top", 8.2 + "em");
   		 }

     });


	$('#search-filters-wrapper .options label').click(function () {
		$(this).prev().prop("checked", true);
	});

    $("#slider-range").slider({
      range: true,
      min: 0,
      max: 500,
      values: [ 75, 300 ],
      slide: function( event, ui ) {
        $( "#min-price" ).html(ui.values[ 0 ]);
        $( "#max-price" ).html(ui.values[ 1 ]);
      }
    });
     $( "#min-price" ).html($( "#slider-range" ).slider( "values", 0 ));
     $( "#max-price" ).html($( "#slider-range" ).slider( "values", 1 ));


     $(".minus-btn").click(function () {
     	$(this).next($(this).parent().next().slideToggle());
     	$(this).removeClass("minus-btn").addClass("plus-btn");
     });
});