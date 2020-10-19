
$('#loginform').parsley();
$('#frm_Password').parsley();
$('#frm_PasswordEmail').parsley();
$('#frm_ResetPassword').parsley();
$(function () {
    $(".preloader").fadeOut();
});
$('#to-recover').on("click", function () {
    $("#loginform").slideUp();
    $("#recoverform").fadeIn();
});

$("#loginform").submit(function (e) {
    e.preventDefault();
    $(".erorLabel").addClass("invisible");

    $.ajax({
        url: GetBaseURL() + "Login/ValidateUser",
        method: "POST",
        data: $('#loginform').serialize(),
        success: function (response) {
            if (response == "OK") {
                window.location.replace(GetBaseURL() + "Dashboard");
            }
            else {
                alert(response);
                $(".erorLabel").removeClass("invisible");
                $(".errorMessage").text(response);}
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });  
  


});


$("#frm_Password").submit(function (e) {
    e.preventDefault();
    $.ajax({
        url: GetBaseURL() + "Register/UpdatePassword",
        method: "POST",
        data: $('#frm_Password').serialize(),
        success: function (response) {
            if (response == "OK") {
                window.location.replace(GetBaseURL() + "Dashboard");
            }
            else {
                $(".erorLabel").removeClass("invisible");
                $(".errorMessage").text(response);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });



});

$("#frm_ResetPassword").submit(function (e) {
    e.preventDefault();
    $.ajax({
        url: GetBaseURL() + "/Signup/UpdateForgotPassword",
        method: "POST",
        data: $('#frm_ResetPassword').serialize(),
        success: function (response) {
            if (response == "OK") {
                $(".successMessagelabel").removeClass("invisible");
                $(".successMessage").text("Password Changed Successfully");
                setTimeout(function () { window.location.replace(GetBaseURL() + "/Login") }, 2000);
           
                
              
            }
            else {
                $(".erorLabel").removeClass("invisible");
                $(".errorMessage").text(response);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });



});

$("#frm_PasswordEmail").submit(function (e) {
    e.preventDefault();
    $.ajax({
        url: GetBaseURL() + "/Signup/SendPasswordResetLink",
        method: "POST",
        data: $('#frm_PasswordEmail').serialize(),
        success: function (response) {
            if (response == "OK") {
                window.location.replace(GetBaseURL() + "/Signup/ResetPasswordEmailSuccess");
            }
            else {
                $(".erorLabel").removeClass("invisible");
                $(".errorMessage").text(response);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });



});

window.Parsley.addValidator('uppercase', {
    requirementType: 'number',
    validateString: function (value, requirement) {
        var uppercases = value.match(/[A-Z]/g) || [];
        return uppercases.length >= requirement;
    },
    messages: {
        en: 'Your password must contain at least (%s) uppercase letter.'
    }
});

//has lowercase
window.Parsley.addValidator('lowercase', {
    requirementType: 'number',
    validateString: function (value, requirement) {
        var lowecases = value.match(/[a-z]/g) || [];
        return lowecases.length >= requirement;
    },
    messages: {
        en: 'Your password must contain at least (%s) lowercase letter.'
    }
});

//has number
window.Parsley.addValidator('number', {
    requirementType: 'number',
    validateString: function (value, requirement) {
        var numbers = value.match(/[0-9]/g) || [];
        return numbers.length >= requirement;
    },
    messages: {
        en: 'Your password must contain at least (%s) number.'
    }
});

//has special char
window.Parsley.addValidator('special', {
    requirementType: 'number',
    validateString: function (value, requirement) {
        var specials = value.match(/[^a-zA-Z0-9]/g) || [];
        return specials.length >= requirement;
    },
    messages: {
        en: 'Your password must contain at least (%s) special characters.'
    }
});
