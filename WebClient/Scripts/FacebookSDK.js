﻿//MOVE the script? as it will probably get messy with other login providers
//and facebook recomends it stays inside the body
//
//
//please dont change this
var FACEBOOK_CODE = 0;

window.fbAsyncInit = function () {
    FB.init({
        appId: 234060146782596,
        status: true,
        cookie: true, // enable cookies to allow the server to access the session
        xfbml: true  // parse XFBML
    });
    FB.Event.subscribe('auth.authResponseChange', function (response) {
        // Here we specify what we do with the response anytime this event occurs.
        if (response.status === 'connected') {
            // The response object is returned with a status field that lets the app know the current
            // login status of the person. In this case, we're handling the situation where they
            // have logged in to the app.
            var uid = response.authResponse.userID;
            var accessToken = response.authResponse.accessToken;
            getData();

        } else if (response.status === 'not_authorized') {
            // the user is logged in to Facebook, 
            // but has not authenticated your app

        } else {
            // the user isn't logged in to Facebook.

        }
    });
};

function getData() {
    FB.getLoginStatus(function (response) {
        var accessToken = response.authResponse.accessToken;
        $.post("/Auth/registerUser", { 'access_token': accessToken , 'provider' : FACEBOOK_CODE },
        function (data, statusText) {
            var name = data.name;
            var id = data.id;
        });
    });
}

// Load the SDK asynchronously
(function (d) {
    var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
    if (d.getElementById(id)) { return; }
    js = d.createElement('script'); js.id = id; js.async = true;
    js.src = "//connect.facebook.net/en_US/all.js";
    ref.parentNode.insertBefore(js, ref);
}(document));

// Here we run a very simple test of the Graph API after login is successful.
// This testAPI() function is only called in those cases.
function testAPI(uid) {
    console.log('Welcome!  Fetching your information.... ');
    FB.api('/me', function (response) {
        document.getElementById("account_model_fb_id").value = response.id;
        document.getElementById("client_model_name").value = response.name;
        document.getElementById("client_model_address").value = response.hometown.name;
        //document.getElementById("client_model_email").value = response.email;
        console.log('Good to see you, ' + response.name + '.');
        console.log('Username: ' + response.username + '.');
        console.log('May I contact you here: ' + response.email + '.');
        var img_link = "http://graph.facebook.com/" + response.id + "/picture?type=large"
        var img = document.getElementById('user_photo');
        img.src = img_link;
        console.log(response);

    });
    FB.api({
        method: 'fql.query',
        query: 'SELECT name, pic FROM user WHERE uid=' + uid
    },
    function (response) {
        //load profile picture response
        console.log(response);
    });
}