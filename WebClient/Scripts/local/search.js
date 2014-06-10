var params = getUrlVars();
var textSearch = params['textSearch'];

var last_ad_id = $("#last-ad-id").html();
$("#last-ad-id").remove();

var last_inst_id = $("#last-inst-id").html();
$("#last-inst-id").remove();

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

function addAdResultsContent() {
    $.get("/Search/SearchAd?textSearch=" + textSearch + "&last_ad_id=" + last_ad_id, function (data) {
        if (data != []) {
            $("#ad-search-wrapper .col-md-9").append(data);
            html = $.parseHTML(data);

            $.each(html, function (i, el) {
                if (html[i].id != "undefined") {
                    if (html[i].id > last_ad_id) last_ad_id = html[i].id;
                }
            });
        }
    });
}

function addInstitutionResultsContent() {
    $.get("/Search/SearchInstitution?textSearch=" + textSearch + "&last_inst_id=" + last_inst_id, function (data) {
        if (data != []) {
            $("#inst-search-wrapper .col-md-9").append(data);
            html = $.parseHTML(data);

            $.each(html, function (i, el) {
                if (html[i].id != "undefined") {
                    if (html[i].id > last_inst_id) last_inst_id = html[i].id;
                }
            });
        }
    });
}

$(window).scroll(function () {
    if ($(window).scrollTop() == $(document).height() - $(window).height()) {
        if ($('#ad-search-wrapper').css('display') == 'none')
            addInstitutionResultsContent();
        else
            addAdResultsContent();
    }
});

$(document).ready(function () {
	$('#search-filters-wrapper .options label').click(function () {
		$(this).prev().prop("checked", true);
	});

    $("#slider-range").slider({
      range: true,
      min: 0,
      max: 500,
      values: [ 0, 500 ],
      slide: function( event, ui ) {
        $( "#min-price" ).html(ui.values[ 0 ]);
        $( "#max-price" ).html(ui.values[ 1 ]);
      }
    });
     $( "#min-price" ).html($( "#slider-range" ).slider( "values", 0 ));
     $( "#max-price" ).html($( "#slider-range" ).slider( "values", 1 ));


     $(".minus-btn").click(function () {
         $(this).next($(this).parent().next().slideToggle());
         if ($(this).html() == "+")
             $(this).html("-");
         else
             $(this).html("+");  	
     });

     $("#cp-btn").click(function () {
         $("#type-filter .current").removeClass("current");
         $(this).addClass("current");
         $("#inst-search-wrapper").fadeOut(500, function () {
             $("#ad-search-wrapper").fadeIn(500);
         });
     });

     $("#inst-btn").click(function () {
         $("#type-filter .current").removeClass("current");
         $(this).addClass("current");
         $("#ad-search-wrapper").fadeOut(500, function () {
             $("#inst-search-wrapper").fadeIn(500);
         });
     });
});