var lowerLimit = 0;

function addResultsContent() {
    $.get("Home/Ad" + lowerLimit, function (data) {
        
    });
}

$(document).ready(function () {
    $('.slider').glide({
        autoplay: 5000,
        arrows: 'true',
        navigation: '.slider-navigation-controls'
    });

    $(".slider-nav__item").click(function() {
    	$(".slider-nav__item--current").removeClass("slider-nav__item--current");
    	$(this).addClass("slider-nav__item--current");
    });
});