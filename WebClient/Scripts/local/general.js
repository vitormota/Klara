
var currentLatitude = 0;
var currentLongitude = 0;

var currentSelected = 1;

function setCurrentLocation() {
    
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(setPosition);
        
    }
    
    
}

function setPosition(position) {
    currentLatitude = position.coords.latitude;
    currentLongitude = position.coords.longitude;
    //console.log('position: ' + currentLatitude + "; " + currentLongitude);
}

function regionFilter(id) {
    setCurrentLocation();

    var ads = $('.searched-ad');

    ads.each(function () {
    
        var lat = parseInt($(this).children('.latitude-ad').html());
        var long = parseInt($(this).children('.longitude-ad').html());
    
        //console.log(getCurrentPosition());
        var d = distanceInKm(currentLatitude,currentLongitude,lat,long);
        //console.log("distance: " + d);
        
        if (id == 1 && d <= 50) {
            $(this).show();

        } else if (id == 2 && d >= 50 && d <= 100) {
            $(this).show();
        } else if (id == 3 && d >= 100) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}

function distanceInKm(lat1, lon1, lat2, lon2) {
    var R = 6371; // km 
    var dLat = (lat2 - lat1) * Math.PI / 180;
    var dLon = (lon2 - lon1) * Math.PI / 180;
    var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) +
		Math.cos(lat1 * Math.PI / 180) * Math.cos(lat2 * Math.PI / 180) *
		Math.sin(dLon / 2) * Math.sin(dLon / 2);
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    var d = R * c;
    if (d > 1) return Math.round(d);
    else if (d <= 1) return Math.round(d * 1000);
    return d;
}

function updatePriceFilter() {
    console.log('min: ' + $('#slider-range-wrapper').children('#min-price').html());
    var min_price = parseInt($('#slider-range-wrapper').children('#min-price').html());
    var max_price = parseInt($('#slider-range-wrapper').children('#max-price').html());
    var ads = $('.searched-ad');

    ads.each(function () {
        var ad_price = parseInt($(this).children('.add-price').html());
        console.log("min_price: "+min_price+"; max_price: "+max_price+"; ad_price: "+ad_price);
        if (ad_price >= min_price && ad_price <= max_price) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}

function updateSpecialtyFilter(spx) {
    
    var specialty = spx;
    var ads = $('.searched-ad');

    ads.each(function () {
        var ad_specialty = $(this).children('.specialty-ad').html();
        console.log(ad_specialty+" ; "+specialty);
        if (ad_specialty == specialty || specialty == "Todos") {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}


