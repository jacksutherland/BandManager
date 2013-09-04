var ui = {
    slide: null,
    lightBoxSettings: { overlayOpacity: '0.7' }
};

$(document).ready(function () {
    loadContact();

    if ($(".lightbox a").length > 0)
        $(".lightbox a").lightBox(ui.lightBoxSettings);

    ui.slide = $(".slideshow img:first").show();
    nextSlide();
});

function nextSlide() {
    setTimeout(function () {
        ui.slide.fadeOut(function () {
            ui.slide = ui.slide.next();

            if (ui.slide.length === 0) ui.slide = $(".slideshow img:first");

            ui.slide.fadeIn(function () {
                nextSlide();
            });
        });

    }, 5000);
}

function loadContact() {
    $("form#sendmail #btnsend").click(function () {
        var form = $("form#sendmail");
        var name = form.find("#name");
        var phone = form.find("#phone");
        var email = form.find("#email");
        var subject = form.find("#subject");
        var message = form.find("#message");
        var button = form.find("#btnsend");
        var error = form.find(".error");

        var validationMessage = "You must enter your name, email address, and a message.";

        error.text("");

        if ($.trim(name.val()) === "" || $.trim(message.val()) === "" || $.trim(email.val()) === "") {
            error.text(validationMessage);
            return false;
        }

        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!filter.test(email.val())) {
            error.text("You entered an invalid email.");
            return false;
        }

        var nstr = name.val();
        var estr = email.val();
        var pstr = phone.val();
        var mstr = message.val();
        var sstr = subject.val();

        error.text("Sending...");

        name.attr('disabled', 'disabled');
        phone.attr('disabled', 'disabled');
        email.attr('disabled', 'disabled');
        subject.attr('disabled', 'disabled');
        message.attr('disabled', 'disabled');
        button.attr('disabled', 'disabled');

        $.post(
            "sendmail.php",
            {
                name: nstr,
                phone: pstr,
                email: estr,
                subject: sstr,
                message: mstr
            },
            function (data) {
                name.removeAttr('disabled');
                email.removeAttr('disabled');
                phone.removeAttr('disabled');
                subject.removeAttr('disabled');
                message.removeAttr('disabled');
                button.removeAttr('disabled');

                if (data.returnValue === "success") {
                    error.text("Your message was sent successfully. We will contact you shortly. Thanks you!");

                    name.val("");
                    email.val("");
                    phone.val("");
                    message.val("");
                }
                else {
                    error.text("Sorry. We are unable to send at this time.");
                }
            },
            "json"
        );
    });
}

_CF_checkform1 = function (_CF_this) {
    //reset on submit
    _CF_error_exists = false;
    _CF_error_messages = new Array();
    _CF_error_fields = new Object();
    _CF_FirstErrorField = null;

    //form element email 'EMAIL' validation checks
    if (!_CF_checkEmail(_CF_this['email'].value, false)) {
        _CF_onError(_CF_this, "email", _CF_this['email'].value, "Error in email text.");
        _CF_error_exists = true;
    }


    //display error messages and return success
    if (_CF_error_exists) {
        if (_CF_error_messages.length > 0) {
            // show alert() message
            _CF_onErrorAlert(_CF_error_messages);
            // set focus to first form error, if the field supports js focus().
            if (_CF_this[_CF_FirstErrorField].type == "text")
            { _CF_this[_CF_FirstErrorField].focus(); }

        }
        return false;
    } else {
        return true;
    }
}