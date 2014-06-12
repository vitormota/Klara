$(document).on("change", "#culture", function (e) {
    e.preventDefault();

    var cult = $("#culture :selected").val();

    $.post(
        '/Home/SetCulture',
        { culture: cult },
        function (data) {
            location.reload();
        });
});