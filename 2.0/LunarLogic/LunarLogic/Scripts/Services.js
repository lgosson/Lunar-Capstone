$(document).ready(function () {
    // Initialize progress bar
    $('#pb').progressbar();

    // Initialize sidebar and implement functionality
    $('#sidebar').sidebar({
        side: "right"
    });

    $("#sidebartoggle").on("click", function () {
        $("#sidebar").trigger("sidebar:toggle", [{ speed: 350 }]);
    });

    $('#contactform').sidebar({
        side: "right"
    });

    $("#contactbutton").on("click", function () {
        $("#contactform").trigger("sidebar:toggle", [{ speed: 350 }]);
    });

    $("#backtoplan").on("click", function () {
        $("#contactform").trigger("sidebar:toggle", [{ speed: 350 }]);
        $("#sidebar").trigger("sidebar:open", [{ speed: 350 }]);
    });

    // Reset button functionality
    $('#reset').click(function () {
        //$('#contactformlist ul').empty();
    });

    // Input mask on contact form of phone field
    $('#Phone').mask("(999) 999-9999");
});








