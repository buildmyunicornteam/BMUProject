
var ImagePreview = "<div id='my_img' class='dz-preview dz-file-preview dz-error' style='position:relative'><img src='../Content/JTFRP/Images/preview.jpg' width='100%' class='img-responsives img-thumbnail' /></div></div>";

$(document).ready(function () {
    $("li a, li").removeClass("active");
    $("li ul").removeClass("in");
    $("#Businessnav").addClass("active");
    $("#Businessnav ul").addClass("in");
});
 $("#ImageUpload").on('change', function ()  {

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
            url: GetBaseURL() + 'Profile/FileUpload',
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

$("#frm_UpdateProfile").submit(function(e) {

        //prevent Default functionality
        e.preventDefault();
        var BusinessPlacement = [];
        var Worklocation = [];
        $.each($("input[name='_BusinessPlacement']:checked"), function () {
            BusinessPlacement.push($(this).val());

        });
        $.each($("input[name='_WorkLocation']:checked"), function () {
            Worklocation.push($(this).val());
        });
      
        $("#BusinessPlacement").val(BusinessPlacement.join(","));
        $("#WorkLocation").val(Worklocation.join(","));
          $.ajax({
                url:  GetBaseURL() + "Profile/UpdateProfile",
                method: "POST",
                data: $('#frm_UpdateProfile').serialize(),
                success: function (response) {
                 location.reload(); 
                },
                error: function (response) {
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

