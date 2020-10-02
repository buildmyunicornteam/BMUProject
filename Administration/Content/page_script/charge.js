var screenHeight = (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);


$(document).ready(() => {
    $("#StartupGrid").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: {
                    url: "/Startup/GetAllStartupList"

                },
            },
            serverPaging: true,
            schema: {
                type: 'json',
                data: 'msg',
                total: "total",
                model: {
                    id: "ID",
                    fields: {
                        ID: { type: "guid" },
                        Value: { type: "string" },
                        DisplayOrder: { type: "string" },
                        CreateDateTime: { type: "date" },
                        ModifiedDateTime: { type: "date" },
                        CreatedName: { type: "string" },
                        ModifiedName: { type: "string" }
                        // IsActive: { type: "date" },
                        //SMPPUsername: { type: "string" },
                        //CompanyName: { type: "string" },
                    }
                }
            },
            pageSize: 100
        },
        height: (screenHeight > 768) ? screenHeight - 310 : 670,
        sortable: false, groupable: false, filterable: true, reorderable: false, resizable: true, noRecords: true,
        selectable: "row",
        messages: {
            noRecords: "No Start Up Found."
        },
        pageable: { refresh: true, pageSizes: ['All', 20, 35, 50, 100], buttonCount: 5 },
        // dataBound: function () { for (var i = 0; i < $("#StartupGrid").columns.length; i++) { if (i !== 0) { $("#StartupGrid").autoFitColumn(i); } } },

        columns: [{
            template: '<button class="btn btn-info" title="Edit Startup" onclick=Edit("#: ID #")><i class="icon-pencil"></i></button>\
                      <button class= "btn btn-danger" title="Delete Startup" onclick=Delete("#: ID #") > <i class="fa fa-trash"></i></button> ',
            width: 100
        },
        { field: "Value", title: "StartUp", width: 140, filterable: false },


        {
            field: "DisplayOrder",
            title: "DisplayOrder",
            width: 130
        },
        { field: "CreatedName", title: "Created By", width: 120, filterable: false },
        { field: "CreateDateTime", title: "Created Date", format: "{0:dd-MMM-yyyy}", parseFormats: ["MM/dd/yyyy"], width: 120, filterable: false },
        { field: "ModifiedName", title: "Modified By", width: 120, template: '#if(data.ModifiedName === null){#<span class="badge badge-danger"> No one Modified </span>#}else {##: ModifiedName ##  }#', filterable: false },
        { field: "ModifiedDateTime", title: "Modified Date", width: 120, template: '#if(data.ModifiedDateTime === null){#<span class="badge badge-danger"> Never Modified </span>#}else {##: kendo.toString(kendo.parseDate(ModifiedDateTime), "dd-MMM-yyyy") ##  }#', filterable: false }
        ]
    });
});

$('#editStartupForm').parsley();
$('#newStartupForm').parsley();
$("#editStartupForm").submit(function (e) {
    e.preventDefault();


    $.ajax({
        url: GetBaseURL() + "Startup/EditStartup",
        method: "POST",
        data: $('#editStartupForm').serialize(),
        success: function (response) {
            if (response == "OK") {
                $('#editStartupForm')[0].reset();
                $('#Edit-startup-modal').modal('toggle');
                swal("Success!", "Startup Updated Successfully", "success");
                $('#StartupGrid').data('kendoGrid').dataSource.read()


            }
            else {
                swal("Error!", response, "error");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });



});

$("#newStartupForm").submit(function (e) {
    e.preventDefault();


    $.ajax({
        url: GetBaseURL() + "Startup/AddStartup",
        method: "POST",
        data: $('#newStartupForm').serialize(),
        success: function (response) {
            if (response == "OK") {
                $('#newStartupForm')[0].reset();
                $('#add-startup-modal').modal('toggle');
                swal("Success!", "Startup Added Successfully", "success");
                $('#StartupGrid').data('kendoGrid').dataSource.read()


            }
            else {
                swal("Error!", response, "error");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });



});


function Edit(Value) {


    $.ajax({
        url: GetBaseURL() + "Startup/GetStartup/",
        method: "GET",
        data: { ID: Value },
        dataType: 'json',
        success: function (data) {

            $("#ID").val(data.ID);
            $("#Value").val(data.Value);
            $("#DisplayOrder").val(data.DisplayOrder);
            $('#Edit-startup-modal').modal('toggle');
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });


}

function Delete() {


    alert();
    $.confirm({
        title: 'Confirmation?',
        content: 'Delete  Request Will Automatically  \'cancel\' in 6 seconds if you don\'t respond.',
        autoClose: 'cancelAction|8000',
        escapeKey: true,
        backgroundDismiss: false,
        typeAnimated: true,
        buttons: {
            deleteUser: {
                text: 'Delete',
                btnClass: 'btn-red',
                action: function () {
                    var result = CommonFunctions.AjaxCall("Post", "/Loan/DeleteLoan", { LoanID: LoanID }, 'json', 'Error Deleting Loan!');
                    switch (result) {
                        case 'OK': new PNotify({ title: 'Success!', text: "Loan  Deleted Successfully", type: 'success', styling: 'fontawesome' });

                            Grid.dataSource.read();
                            break;
                        default: swal({ title: "Error!", text: result, type: "error", confirmButtonColor: "#DD6B55", showConfirmButton: true }); break;
                    }



                }
            },
            cancelAction: function () {
                $.alert('Delete Request is Canceled');
            }
        }
    });




}
