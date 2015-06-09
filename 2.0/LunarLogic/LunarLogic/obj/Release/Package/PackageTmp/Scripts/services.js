$(document).ready(function () {
    // Initialize progress bar
    $('#pb').progressbar();

    // Initialize sidebar and implement functionality
    $('#sidebar').sidebar({
        side: "right"
    });

    // Initialize contact form sidebar
    $('#contactform').sidebar({
        side: "right"
    });

    // Contact button to trigger the contact form opening
    $("#contactbutton").on("click", function () {
        $("#contactform").trigger("sidebar:toggle", [{ speed: 350 }]);
    });

    // Back to plan button to trigger the personal plan opening
    $("#backtoplan").on("click", function () {
        $("#contactform").trigger("sidebar:toggle", [{ speed: 350 }]);
        $("#sidebar").trigger("sidebar:open", [{ speed: 350 }]);
    });

    // Input mask on contact form of phone field
    $('#Phone').mask("(999) 999-9999");
});








