$("#jsMainList").getList();
$(document).ready(function () {
   
    $("li a, li").removeClass("active");
    $("li ul").removeClass("in");
    $("#IdeaValidatenav").addClass("active");
    $("#IdeaValidatenav ul").addClass("in");
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
    $(document).on("click", ".jsDeleteSurvey", function () {
        var SurveyID = $(this).data("survey-id");
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
                        url: "/Questionnaire/DeleteSurvey",
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


// Initialize the formio builder configuration (optional)
// Default cartegories: basic, advanced, layout, data
const options = {
    builder: {
        basic: false,
        advanced: false,
        data: false,

        custom: {
            title: 'BuildMyUnicorn',
            weight: 10,
            default: true,
            components: {
                TextField: {
                    title: 'Text Field',
                    key: 'Question',
                    icon: 'terminal',
                    schema: {
                        label: 'Question',
                        type: 'textfield',
                        key: 'Question',
                        input: true
                    }
                },

                email: {
                    title: 'Email',
                    key: 'email',
                    icon: 'at',
                    schema: {
                        label: 'Question',
                        type: 'email',
                        key: 'email',
                        input: true
                    }
                },
                phoneNumber: {
                    title: 'Mobile Phone',
                    key: 'mobilePhone',
                    icon: 'phone-square',
                    schema: {
                        label: 'Question',
                        type: 'phoneNumber',
                        key: 'mobilePhone',
                        input: true
                    }
                },
                textarea: {
                    title: 'Text Area',
                    key: 'textarea',
                    icon: 'phone-square',
                    schema: {
                        label: 'Question',
                        type: 'textarea',
                        key: 'textarea',
                        input: true
                    }
                },
                checkbox: {
                    title: 'Checkbox',
                    key: 'checkbox',
                    icon: 'phone-square',
                    schema: {
                        label: 'Question',
                        type: 'checkbox',
                        key: 'checkbox',
                        input: true
                    }
                },
                selectboxes: {
                    title: 'Select Boxes',
                    key: 'selectboxes',
                    icon: 'phone-square',
                    schema: {
                        label: 'Question',
                        type: 'selectboxes',
                        key: 'selectboxes',
                        input: true
                    }
                },
                select: {
                    title: 'Select',
                    key: 'select',
                    icon: 'phone-square',
                    schema: {
                        label: 'Question',
                        type: 'select',
                        key: 'select',
                        input: true
                    }
                },
                radio: {
                    title: 'Radio',
                    key: 'radio',
                    icon: 'phone-square',
                    schema: {
                        label: 'Question',
                        type: 'radio',
                        key: 'radio',
                        input: true
                    }
                },
                button: true
            }
        },
        layout: {
            components: {
                table: false
            }
        }

    }
};
// Initialize the form.io form JSON object
const form = {
    components: [
        //{
        //    key: 'textfield',
        //    type: 'textfield',
        //    validate: {
        //        required: true
        //    }
        //},
        //{
        //    key: 'datetime',
        //    type: 'datetime'
        //},
        //{
        //    key: 'submit',
        //    type: 'button',
        //    theme: 'primary'
        //}
    ]
};

const formio = {
    builder: null,
    form: null
};

//Initialize the formio form instance
Formio.createForm(document.getElementById('formio-form'), form)
    .then((instance) => {
        formio.form = instance;

    });

// Initialize the formio builder instance
Formio.builder(document.getElementById('formio-builder'), form, options)
    .then((instance) => {
        formio.builder = instance;

        // Define the on render event of the formio builder instance
        formio.builder.on('render', () => {
            // Update the formio form object and re render the form
             formio.form.form = form;
             formio.form.render();
            builderSchema = formio.builder.schema

            // Update the json code using Prism.js
            document.querySelector('code').innerHTML = Prism.highlight(JSON.stringify(form, null, 2), Prism.languages.json, 'json');
            $("#SurveyForm").val(JSON.stringify(form, null, 2), Prism.languages.json, 'json');
        });
        document.querySelector('code').innerHTML = Prism.highlight(JSON.stringify(form, null, 2), Prism.languages.json, 'json');
    });

Formio.icons = 'fontawesome';



$("#MyForm").submit(function (e) {
    e.preventDefault();
    $('#MyForm input[type="text"]').each(function () {
        alert($(this).attr('name'));
        alert($(this).val());
    });
    $('#MyForm select[type="text"]').each(function () {
        alert($(this).attr('name'));
        alert($(this).val());
    });

});
function copyToClipboard(element) {
   
    var copyText = document.getElementById(element);
    copyText.type = 'text';
    copyText.select();
    document.execCommand("copy");
    copyText.type = 'hidden';
    toastMessage("Copy", "success", "Survey link copyied to clipboard");
   
}

function toastMessage(heading,icon, message)
{
    $.toast({
        heading: heading,
        text: message,
        position: 'top-right',
        loaderBg: '#ff6849',
        icon: icon,
        hideAfter: 1500,
        stack: 18
    });
}