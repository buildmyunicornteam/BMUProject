
$('#todoform').submit(function () {
    event.preventDefault();
    if ($(this).valid) {
    var formData = new FormData(this);
    $.ajax({
        url: this.action,
        method: 'post',
        contentType: false,
        processData: false,
        data: formData,
        success: function (res, status, xhr) {
            toastr.success(
                'To-do Task!',
                res.Message,
                {
                    timeOut: 1000,
                    fadeOut: 1000,
                    onHidden: function () {
                        window.location.href = "/todo/edittodo/" + res.EntityID;
                    }
                }
            );
        },
        error: handleError
    });
     }
    else
    return false;
});

$('.delete').click(function () {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this record!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                var id = $(this).attr("data-id");
                $.ajax({
                    url: "/todo/DeleteTodo",
                    method: 'post',
                    data: { id },
                    success: function (res, status, xhr) {
                        toastr.success(
                            'To-do Task!',
                            'Todo deleted successfully');
                        $('tbody').html(res);
                    },
                    error: handleError
                });
            }
        });

  
})

function handleError(xhr) {
    var response = JSON.parse(xhr.responseText);
    toastr.error(response.Error);
};

