$(document).on("click", "#changeLang", function (e) {
    e.preventDefault();

    var cult = $("#culture :selected").val();

    $.post(
        '/Home/SetCulture',
        { culture: cult },
        function (data) {
            location.reload();
        });
});