$(document).ready(function () {
    $(".profile-btn").click(function () {
        $(this).hide();
        $("#edit-details input.editable").removeAttr("readonly");
        $("#user-details input[type=\"submit\"]").show();
    });

    $("#user-details input[type=\"submit\"]").click(function (e) {
        e.preventDefault();

        var userInfo = {
            address: $("#user-details #user_address").val(),
            phone_number: $("#user-details #user_phone_number").val(),
            nif: $("#user-details #user_nif").val(),
            city: $("#user-details #user_city").val()
        };

        $.ajax({
            url: "/User/EditDetails",
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(userInfo),
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                $("#user-details input[type=\"submit\"]").hide();
                $("#edit-details input.editable").prop('readonly', true);
                $(".profile-btn").show();
            }
        });
    });
});