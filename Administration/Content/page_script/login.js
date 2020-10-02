
$('#loginForm').parsley();
$("#loginForm").submit(function (e) {
    e.preventDefault();


    $.ajax({
        url: GetBaseURL() + "Login/ValidateUser",
        method: "POST",
        data: $('#loginForm').serialize(),
        success: function (response) {
            if (response == "OK") {
                window.location.replace(GetBaseURL() + "Dashboard");
            }
            else {
                alert(response);
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