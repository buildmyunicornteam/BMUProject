
var CommonFunctions = new function () {

    this.AjaxCall = function (MethodType, URL, Data, DataType, ErrorMessage, contentType) {
        var ReturnResult = "";
        $.ajax({
            type: MethodType,
            url: URL,
            data: Data,
            datatype: DataType,
            async: false,
            cache: false,
            contentType: contentType,

            success: function (result) {
                ReturnResult = result;
            },
            error: function (jqXHR, exception) {
                ReturnResult = "ERROR";
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
             
            }
        });
        return ReturnResult;
    };

    this.SuccessMessage = function (Title, Message) {

        

    };

    this.ErrorMessage = function (Title, Message) {

      

    };

  
   

    this.GetQueryStringValue = function () {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    };
};






function isNullAndUndef(variable) {

    return (variable === null || typeof variable === 'undefined' || variable === "");
}