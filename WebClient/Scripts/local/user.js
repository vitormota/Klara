$(document).ready(function () {   
    var badge1 = false;
    $("#div-badge1").click(function () {
        if (!badge1) {
            $(".glyphicon-chevron-down").removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-right");
            badge2 = false;
            badge3 = false;
            badge4 = false;
            $("#dp-badge1").removeClass("glyphicon-chevron-right").addClass("glyphicon-chevron-down");
            badge1 = true;
        } else {
            $("#dp-badge1").removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-right");
            badge1 = false;
        }
    });

    var badge2 = false;
    $("#div-badge2").click(function () {
        if (!badge2) {
            $(".glyphicon-chevron-down").removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-right");
            badge1 = false;
            badge3 = false;
            badge4 = false;
            $("#dp-badge2").removeClass("glyphicon-chevron-right").addClass("glyphicon-chevron-down");
            badge2 = true;
        } else {
            $("#dp-badge2").removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-right");
            badge2 = false;
        }
    });

    var badge3 = false;
    $("#div-badge3").click(function () {
        if (!badge3) {
            $(".glyphicon-chevron-down").removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-right");
            badge1 = false;
            badge2 = false;
            badge4 = false;
            $("#dp-badge3").removeClass("glyphicon-chevron-right").addClass("glyphicon-chevron-down");
            badge3 = true;
        } else {
            $("#dp-badge3").removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-right");
            badge3 = false;
        }
    });

    var badge4 = false;
    $("#div-badge4").click(function () {
        if (!badge4) {
            $(".glyphicon-chevron-down").removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-right");
            badge1 = false;
            badge2 = false;
            badge3 = false;
            $("#dp-badge4").removeClass("glyphicon-chevron-right").addClass("glyphicon-chevron-down");
            badge4 = true;
        } else {
            $("#dp-badge4").removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-right");
            badge4 = false;
        }
    });

    $(".subscribed-group .date").click(function () {
        $(this).parent().children(".subscribed-ad").slideToggle();
    });

    $(".buyed-group .date").click(function () {
        $(this).parent().children(".buyed-ad").slideToggle();
    });

    $(".profile-btn").click(function () {
        $("#edit-details span").hide();
        $(this).hide();
        $("#edit-details input").show();
    });

    $("input[type=\"submit\"").click(function (e) {
        e.preventDefault();
        
        var userInfo = {
            address: $("#edit-details #address").val(),
            phone_number: $("#edit-details #phone_number").val(),
            nif : $("#edit-details #nif").val(),
            city: $("#edit-details #city").val()
        };

        $.ajax({
            url: "/User/EditDetails",
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(userInfo),
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                $("input[type=\"text\"").each(function (index) {
                    $(this).prev("span").html($(this).val());
                });

                $("input").hide();
                $("#edit-details span").show();
                $(".profile-btn").show();
            }
        });
    });

    $(document).on("click", ".subscribed-inst, .buyed-ad, .subscribed-ad", function (e) {
        document.location = $(this).children("a").attr('href');
    });
});