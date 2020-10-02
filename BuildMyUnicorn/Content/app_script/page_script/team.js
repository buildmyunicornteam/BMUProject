   var ImagePreview = "<div id='my_img' class='dz-preview dz-file-preview dz-error' style='position:relative'><img src='../Content/JTFRP/Images/preview.jpg' width='100%' class='img-responsives img-thumbnail' /></div></div>";

$(document).ready(function () {
    $("li a, li").removeClass("active");
    $("li ul").removeClass("in");
    $("#Businessnav").addClass("active");
    $("#Businessnav ul").addClass("in");

});
    $("#ImageUpload").on('change', function () {
    
        var form_Data = new FormData();

        var fileUpload = $("#ImageUpload").get(0);
        var files = fileUpload.files;  

        if (files.length > 0) {
            for (var i = 0; i < files.length; i++) {
                var fileName = files[i].name;
                var flExt = fileName.substring(fileName.lastIndexOf(".") + 1, fileName.length);
                if ($.inArray(flExt.toLowerCase(), ['gif', 'png', 'jpg', 'bmp', 'jpeg']) !== -1)
                    form_Data.append("file", files[i]);
                else { alert("Invalid File");return false; }
            }
        }
     
        $.ajax({
            type: "POST",
            url: '/Team/FileUpload',
            data: form_Data,
            cache: false,
            contentType: false,
            processData: false,
            async: false,
            success: function (data) {

                if (data != "!OK") {
                    $("#ImageID").val(data);
                    $('#imagepreview').html("<div id='my_img' class='dz-preview dz-file-preview dz-error' style='position:relative'><img src='../Content/file_upload/" + data + "' width='100%' class='img-responsives img-thumbnail' style='height:100%' /><div class='dz-error-mark'><span>X</span></div></div>");
                }

            },
            error: function (jqXHR, exception) {
                var msg = '';
                if (jqXHR.status === 0) {
                    msg = 'Not Connected, Verify Network/Internet.';
                } else if (jqXHR.status === 404) {
                    msg = 'Requested page not found. [404]';
                } else if (jqXHR.status === 500) {
                    msg = 'Internal Server Error [500].';
                } else if (exception === 'parsererror') {
                    msg = 'Requested JSON parse failed.';
                } else if (exception === 'timeout') {
                    msg = 'Time out error.';
                } else if (exception === 'abort') {
                    msg = 'Ajax request aborted.';
                } else if (jqXHR.status === 403) {
                    msg = 'Access Denied. Contact Your Administrator.';
                } else {
                    msg = 'Uncaught Error.\n' + jqXHR.responseText;
                }
                sweetAlert("Error in Uploading Image!", msg, "error");
            }
        });

    });

 $("#EditImageUpload").on('change', function () {


        var form_Data = new FormData();

        var files = $(this)[0].files;

        if (files.length > 0) {
            for (var i = 0; i < files.length; i++) {
                var fileName = files[i].name;
                var flExt = fileName.substring(fileName.lastIndexOf(".") + 1, fileName.length);


                if ($.inArray(flExt.toLowerCase(), ['gif', 'png', 'jpg', 'bmp', 'jpeg']) !== -1)
                    form_Data.append("file", files[i]);
            }
        }

        $.ajax({
            type: "POST",
            url: '/ProjectPhotograph/AddProjectTempImage',
            data: form_Data,
            cache: false,
            contentType: false,
            processData: false,
            async: false,
            success: function (data) {

                $('#Editimagepreview').html("<div id='my_img' class='dz-preview dz-file-preview dz-error' style='position:relative'><img src='" + data + "' width='100%' class='img-responsives img-thumbnail' /><div class='dz-error-mark'><span>X</span></div></div>");


            },
            error: function (jqXHR, exception) {
                var msg = '';
                if (jqXHR.status === 0) {
                    msg = 'Not Connected, Verify Network/Internet.';
                } else if (jqXHR.status === 404) {
                    msg = 'Requested page not found. [404]';
                } else if (jqXHR.status === 500) {
                    msg = 'Internal Server Error [500].';
                } else if (exception === 'parsererror') {
                    msg = 'Requested JSON parse failed.';
                } else if (exception === 'timeout') {
                    msg = 'Time out error.';
                } else if (exception === 'abort') {
                    msg = 'Ajax request aborted.';
                } else if (jqXHR.status === 403) {
                    msg = 'Access Denied. Contact Your Administrator.';
                } else {
                    msg = 'Uncaught Error.\n' + jqXHR.responseText;
                }
                sweetAlert("Error in Uploading Image!", msg, "error");
            }
        });

    });

var actionType = "return";

 $(document).on("click", ".jsSubmit", function () {

   
    if ($(this).hasClass("reload"))
        actionType = "reload";
     

});

 $("#frm_ClientTeam").submit(function (e) {
       
         e.preventDefault();
         var RoleInCompany = [];
        $.each($("input[name='_RoleInCompany']:checked"), function () {
            RoleInCompany.push($(this).val());
        });
        $("#RoleInCompany").val(RoleInCompany);
        $.ajax({
            url: GetBaseURL() + "Team/AddClientTeam",
            method: "POST",
            data: $('#frm_ClientTeam').serialize(),
            success: function (response) {
              
              
                if (response == "OK") {
                    $.toast({
                        heading: 'Success',
                        text: 'Team Added Successfully',
                        position: 'top-right',
                        loaderBg: '#ff6849',
                        icon: 'success',
                        hideAfter: 3500,
                        stack: 6
                    });
                   
                    if (actionType == "return") {
                        actionType = "return";
                        setTimeout(function () { window.location.replace(GetBaseURL() + "Team") }, 2000);
                    }
                    else {
                        $('#frm_ClientTeam')[0].reset();
                        actionType = "return";
                        $('#imagepreview').html("<div id='my_img' class='dz-preview dz-file-preview dz-error' style='position:relative'><img src='../Content/file_upload/profile-dummy.png' width='100%' class='img-responsives img-thumbnail' style='height:100%' /></div>");
                    }
                       
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $(".erorLabel").removeClass("invisible");
                $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
            }
        });



 });

$("#frm_UpdateClientTeam").submit(function (e) {

    e.preventDefault();
    var RoleInCompany = [];
    $.each($("input[name='_RoleInCompany']:checked"), function () {
        RoleInCompany.push($(this).val());
    });
    $("#RoleInCompany").val(RoleInCompany);
    $.ajax({
        url: GetBaseURL() + "Team/UpdateClientTeam",
        method: "POST",
        data: $('#frm_UpdateClientTeam').serialize(),
        success: function (response) {


            if (response == "OK") {
                $.toast({
                    heading: 'Success',
                    text: 'Record Updated Successfully',
                    position: 'top-right',
                    loaderBg: '#ff6849',
                    icon: 'success',
                    hideAfter: 3500,
                    stack: 6
                });

                setTimeout(function () { location.reload(); }, 2000);

            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });



});

$("#frm_UpdateClientProfile").submit(function (e) {

    e.preventDefault();
    var RoleInCompany = [];
    $.each($("input[name='_RoleInCompany']:checked"), function () {
        RoleInCompany.push($(this).val());
    });
    $("#RoleInCompany").val(RoleInCompany);
    $.ajax({
        url: GetBaseURL() + "Team/UpdateClientProfile",
        method: "POST",
        data: $('#frm_UpdateClientProfile').serialize(),
        success: function (response) {


            if (response == "OK") {
               
              
                $.toast({
                    heading: 'Success',
                    text: 'Record Updated Successfully',
                    position: 'top-right',
                    loaderBg: '#ff6849',
                    icon: 'success',
                    hideAfter: 3500,
                    stack: 6
                });
                $('#dialogClientUpdate').modal('hide');
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });



});


  $('input[type="checkbox"]').click(function () {
    var id = $(this).attr("id");
    var name = $(this).attr("name");
    if ($(this).prop("checked") == true) {
        $(this).prop("checked", true);
        $(this).attr('checked', 'checked');
    }
    else if ($(this).prop("checked") == false) {
        $(this).prop("checked", false);
        $(this).removeAttr('checked', 'checked');
        

    }
   
   
});
