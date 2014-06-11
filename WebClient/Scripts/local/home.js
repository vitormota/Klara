var lowerLimit = 10;

function addResultsContent() {
    $.get("Home/Ad?lowerLimit=" + lowerLimit, function (data) {
        $(".home-results").append(data);
        lowerLimit += 10;
    });
}

$(window).scroll(function () {
    if ($(window).scrollTop() == $(document).height() - $(window).height()) {
        addResultsContent();
    }
});

$(document).ready(function () {
    $('.slider').glide({
        autoplay: 0,
        arrows: 'true',
        navigation: '.slider-navigation-controls'
    });

    $(".slider-nav__item").click(function () {
        $(".slider-nav__item--current").removeClass("slider-nav__item--current");
        $(this).addClass("slider-nav__item--current");
    });

    $('.search-box form').submit(function (e) {
        if($('.search-box #textbox').val() == "") {
            e.preventDefault();
        }
    });
});