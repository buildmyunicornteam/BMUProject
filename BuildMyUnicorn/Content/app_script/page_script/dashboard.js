var Model;

$("#IdeaProgress").css("width", "90% !important");
$.get(GetBaseURL() + "Dashboard/GetClientIdeaProgressData", function (data) {

    Model = data;
    $("#IdeaProgress").css("width", "90% !important");
    new Chart(document.getElementById("chart-idea-progress"),
        {
            "type": "bar",
           
            "barThickness": "1px",
            
           
            "data": {
                "labels": ["Your Idea", "Idea Break Down", "About you", "The Company", "Selling Idea", "The Money", "Get The Idea out of my head"],
                "datasets": [{
                    "label": "Percentage Completed",
                    "data": [Model.YourIdeaProgressData, Model.IdeaBreakDownProgressData, Model.AboutYouProgressData, Model.CompanyProgressData, Model.IdeaSellingProgressData, Model.MoneyProgressData, Model.TotalProgressData],
                    "fill": false,
                    "backgroundColor": ["rgba(255, 99, 132, 0.2)", "rgba(255, 159, 64, 0.2)", "rgba(255, 205, 86, 0.2)", "rgba(75, 192, 192, 0.2)", "rgba(54, 162, 235, 0.2)", "rgba(153, 102, 255, 0.2)", "rgba(201, 203, 207, 0.2)"],
                    "borderColor": ["rgb(255, 99, 132)", "rgb(255, 159, 64)", "rgb(255, 205, 86)", "rgb(75, 192, 192)", "rgb(54, 162, 235)", "rgb(153, 102, 255)", "rgb(201, 203, 207)"],
                    "borderWidth": 0,
                   
              
                   

                }
                ]
            },
            "options": {
                "scales": { "yAxes": [{ "ticks": { "beginAtZero": true } }] }
            }
        });

});

$(document).ready(function () {

    $("li a, li").removeClass("active");
    $("li ul").removeClass("in");
    $("#Dashboardnav").addClass("active");
});
$("#frm_UpdateProfile").submit(function (e) {

    e.preventDefault();
    var RoleInCompany = [];
    $.each($("input[name='_RoleInCompany']:checked"), function () {
        RoleInCompany.push($(this).val());

    });


    $("#RoleInCompany").val(RoleInCompany.join(","));
    $.ajax({
        url: GetBaseURL() + "Team/UpdateClientProfile",
        method: "POST",
        data: $('#frm_UpdateProfile').serialize(),
        success: function (response) {
            location.reload();
        },
        error: function (response) {
        }
    });


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
