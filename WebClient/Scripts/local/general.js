
var currentLatitude = 0;
var currentLongitude = 0;

var currentSelected = 1;

function regionFilter(id) {
    
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

