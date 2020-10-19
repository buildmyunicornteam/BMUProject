$("#jsMainList").getList();
$(document).ready(function () {
   
    $("li a, li").removeClass("active");
    $("li ul").removeClass("in");
    $("#Packagenav").addClass("active");
    $("#Packagenav ul").addClass("in");
    
    $("#frm_AddPackage").submit(function(e) {

        //prevent Default functionality
        e.preventDefault();
        var PackageAttribute = [];
         $(".packageAttribute").each(function() {

           PackageAttribute.push($(this).val());
         });
       $("#PackageAttribute").val(PackageAttribute.join(","));
      $.ajax({
            url:  GetBaseURL() + "Package/AddPackage",
            method: "POST",
            data: $('#frm_AddPackage').serialize(),
            success: function (response) {
              window.location.replace(GetBaseURL() + "Package");
               // $("#dialogPackageAdd").modal("toggle");
            },
            error: function (response) {
            }
        });

      
    });
    $("#btnAddSurvey").click(function () {
        var SurveyTitle = $("#SurveyTitle").val();
        var SurveyForm = $("#SurveyForm").val();
        if (SurveyTitle == "") {
            toastMessage("Required", "danger", "Survey title is required");
            return false;
        }
        if (SurveyForm == "") {
            toastMessage("Required", "danger", "Survey form is  required");
            return false;
        }
        var Model = { "SurveyTitle": SurveyTitle, "SurveyForm": SurveyForm };
        $.ajax({
            url: GetBaseURL() + "Questionnaire/AddSurvey",
            type: "POST",
            data: JSON.stringify(Model),
            contentType: "application/json",
            dataType: "json",
           success: function (response) {
                if (response == "OK") {
                    toastMessage("Survey Created", "success", "Survey form created successfully");
                    setTimeout(function () { window.location.replace(GetBaseURL() + "Questionnaire"); }, 2000);
                }
                else {
                    toastMessage("Failed", "danger", response);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                toastMessage("Survey Created", "success", "Survey form created successfully");
                setTimeout(function () { window.location.replace(GetBaseURL() + "Questionnaire"); }, 2000);
              //  toastMessage("Failed", "danger", textStatus + " " + errorThrown);
            }
        });
    });
    $(document).on("click", ".jsDeletePackage", function () {
        var SupplierPackageID = $(this).data("supplierackageid");
        swal({
            title: "Are you sure?",
            text: "Once deleted, you will not be able to recover the record!",
            //icon: "warning",
            buttons: true,
            dangerMode: true,
        })
            .then((willDelete) => {
                if (willDelete) {
                    $.ajax({
                        type: "POST",
                        url: "/Package/DeletePackage",
                        data: { SupplierPackageID: SupplierPackageID },
                        success: function (data) {
                            if (data !== "OK") {
                               // $("tr." + SurveyID).addClass("hide");
                               // sweetAlert("Error Deleting Role!", data, "error");
                            }
                            $("#jsMainList").getList();
                            //else {
                            //    swal("Deleted!", "Role Deleted Successfully", "success");
                            //    $('#tbl_roles').data('kendoGrid').dataSource.read();
                            //}
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
                           // sweetAlert("Error Deleting Role!", msg, "error");
                        }
                    });
                    //swal("Poof! Your imaginary file has been deleted!", {
                    //    icon: "success",
                    //});
                } 
            });
        
    });

    $(document).on("click", ".jsActiveSurvey", function () {
        var SurveyID = $(this).data("survey-id");
        swal({
            title: "Are you sure?",
            text: "Once Active, your survey will be available to user(s)!",
            //icon: "warning",
            buttons: true,
            dangerMode: false,
        })
            .then((willDelete) => {
                if (willDelete) {
                    $.ajax({
                        type: "POST",
                        url: "/Questionnaire/EditSurveyStatus",
                        data: { SurveyID: SurveyID },
                        success: function (data) {
                            if (data !== "OK") {
                                // $("tr." + SurveyID).addClass("hide");
                                // sweetAlert("Error Deleting Role!", data, "error");
                            }
                            $("#jsMainList").getList();
                            
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
                            // sweetAlert("Error Deleting Role!", msg, "error");
                        }
                    });
                    //swal("Poof! Your imaginary file has been deleted!", {
                    //    icon: "success",
                    //});
                }
            });

    });

    $(document).on("click", ".jsInActiveSurvey", function () {
        var SurveyID = $(this).data("survey-id");
        swal({
            title: "Are you sure?",
            text: "Once in active, your survey will be not available to user(s)!",
           // icon: "warning",
            buttons: true,
            dangerMode: false,
        })
            .then((willDelete) => {
                if (willDelete) {
                    $.ajax({
                        type: "POST",
                        url: "/Questionnaire/EditSurveyStatus",
                        data: { SurveyID: SurveyID },
                        success: function (data) {
                            if (data !== "OK") {
                                // $("tr." + SurveyID).addClass("hide");
                                // sweetAlert("Error Deleting Role!", data, "error");
                            }
                            $("#jsMainList").getList();
                            //else {
                            //    swal("Deleted!", "Role Deleted Successfully", "success");
                            //    $('#tbl_roles').data('kendoGrid').dataSource.read();
                            //}
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
                            // sweetAlert("Error Deleting Role!", msg, "error");
                        }
                    });
                    //swal("Poof! Your imaginary file has been deleted!", {
                    //    icon: "success",
                    //});
                }
            });

    });
});

