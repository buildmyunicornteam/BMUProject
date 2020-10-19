// *Plugin for Getting List*
(function ($) {

    $.fn.getList = function () {
        var element = $(this);
        if (typeof (element) != 'undefined' || element != null) {
            var controller = $(element).data('controller');
            var action = $(element).data('action');
        }
        var url = GetBaseURL() + controller + "/" + action;
        $.ajax({
            type: "GET",
            url: url,
            dataType: "html",
            success: function (r) {
                $(element).html(r);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log("Status: " + textStatus); console.log("Error: " + errorThrown);
            }
        });
    }

}(jQuery));
