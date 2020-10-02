bind_Country();

$('#Signupform').parsley();

$("#Signupform").submit(function (e) {
    e.preventDefault();
    if ($("#Agree").prop("checked") == false) { return false; }
    $.ajax({
        url: GetBaseURL() + "Register/AddCustomer",
        method: "POST",
        data: $('#Signupform').serialize(),
        success: function (response) {
       
            if (response == "OK") {
              //  $('#frm_Signup').trigger("reset");
                window.location.replace(GetBaseURL() + "Register/SignupSuccess");
            }
            else {

                alert("Client already exists");
                //$(".erorLabel").removeClass("invisible");
                //$(".errorMessage").text(response);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        } 
    });  


});

$('#customCheck1').click(function () {
    if ($(this).prop("checked") == true) {
        $("#customCheck1").prop("checked", true);
        $("#customCheck1").attr('checked', 'checked')
    }
    else if ($(this).prop("checked") == false) {
        $("#customCheck1").prop("checked", false);
        $("#customCheck1").removeAttr('checked', 'checked')
    }
});

function bind_Country() {
    $.ajax({
        url: GetBaseURL() + "Register/GetCountryList",
        type: 'GET',
        dataType: 'json',
        success: function (res) {
            console.log(res);

            var options = "<option value=''>--Select your Country--</option>";
            $.each(res.country, function (key, val) {

                options += "<option value=" + val.CountryID + ">" + val.CountryName + "</option>";
            });
            $('#CountryID').html(options).trigger('change');
        }
    });

}

  


