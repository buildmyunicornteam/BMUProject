
//console.log($("#jsIdea").val());


var Model;

$.get(GetBaseURL() + "Dashboard/GetClientIdeaProgressData", function (data) {

    Model = data;
    
    if (document.getElementById("chart-idea-progress") != null) {
        new Chart(document.getElementById("chart-idea-progress"),
            {
                "type": "pie",
                "data": {
                    "labels": ["Your Idea", "Idea Break Down", "About you", "The Company", "Selling Idea", "The Money", "Get The Idea out of my head"],
                    "datasets": [{
                        "label": "Percentage Completed",
                        "data": [Model.YourIdeaProgressData, Model.IdeaBreakDownProgressData, Model.AboutYouProgressData, Model.CompanyProgressData, Model.IdeaSellingProgressData, Model.MoneyProgressData, Model.TotalProgressData],
                        "fill": false,
                        "backgroundColor": ["rgba(255, 99, 132, 0.2)", "rgba(255, 159, 64, 0.2)", "rgba(255, 205, 86, 0.2)", "rgba(75, 192, 192, 0.2)", "rgba(54, 162, 235, 0.2)", "rgba(153, 102, 255, 0.2)", "rgba(201, 203, 207, 0.2)"],
                        "borderColor": ["rgb(255, 99, 132)", "rgb(255, 159, 64)", "rgb(255, 205, 86)", "rgb(75, 192, 192)", "rgb(54, 162, 235)", "rgb(153, 102, 255)", "rgb(201, 203, 207)"],
                        "borderWidth": 1
                    }
                    ]
                },
                "options": {
                    "scales": { "yAxes": [{ "ticks": { "beginAtZero": true } }] }
                }
            });
    }

});


$(document).ready(function () {
  
    $("li a, li").removeClass("active");
    $("li ul").removeClass("in");
    $("#Ideanav").addClass("active");
    $("#Ideanav ul").addClass("in");
    //bg-inverse
    $('input[type="checkbox"]').click(function () {
        var id = $(this).attr("id"); 
        var name = $(this).attr("name");
        if ($(this).prop("checked") == true) {
            $(this).prop("checked", true);
            $(this).attr('checked', 'checked');
            //$("." + id).val(id).trigger("change");
        }
        else if ($(this).prop("checked") == false) {
            $(this).prop("checked", false);
            $(this).removeAttr('checked', 'checked');
            //$("." + id).val("").trigger("change");
           
        }
        var _anyCheckedInGroup = false;
        var _id="";
        $.each($("input[name='"+name+"']"), function () {
            if ($(this).prop("checked") == true) {
                _anyCheckedInGroup = true;
                _id = $(this).attr("id");
            
            }


        });
        $("#_" + name).val(_id).trigger("change");
        //if (_anyCheckedInGroup) {  }
    });

    $(document).on("click", ".JsUpdateIdea", function () {
   
        var IdeaID = $("#IdeaID").val();
        window.location.replace(GetBaseURL() + "Idea?IdeaID=" + IdeaID);
    });

    $(document).on("click", ".JsDownloadIdea", function () {

        $.get(GetBaseURL() + "Idea/DownloadPDF", function (data) {
            if (data.status == "OK")
            {
                window.open(GetBaseURL() + "Content/"+data.fileName);
                
            }
        });
    });
    
   // $('.barChart').barChart({ easing: 'easeOutQuart' });

    let i = 0;
    while (i < 10) {
        task(i);
        i++;
    }
    function task(i) {
        setTimeout(function () {
            $("#one").attr("data-value", i*10);
            $("#two").text(i * 10);
           // $('.barChart').barChart({ easing: 'easeOutQuart' });
        }, 2000 * i);
    
    } 
});




//Custom design form example
$(".tab-wizard").steps({
    headerTag: "h6",
    bodyTag: "section",
    transitionEffect: "fade",
    enableAllSteps: true,
    enablePagination: false,
    titleTemplate: '<span class="step">#index#</span> #title#',
    labels: {
        finish: "Submit"
    },
    onFinished: function (event, currentIndex) {
        console.log($("form").serialize());
        console.log($('form').serializeArray());
        var data = {};
        console.log($("form").serializeArray().map(function (x) { data[x.name] = x.value; }));
        Swal.fire("Form Submitted!", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed lorem erat eleifend ex semper, lobortis purus sed.");

    }
});


var form = $(".validation-wizard").show();

$(".validation-wizard").steps({
    headerTag: "h6",
    bodyTag: "section",
    transitionEffect: "fade",
    enableAllSteps: true,
    enableFinishButton: true,
    enableKeyNavigation:true,
    // stepsOrientation: "vertical",
    enablePagination: true,
    showFinishButtonAlways: true,
    saveState:true,
    titleTemplate: '<span class="step">#index#</span> #title#',
    labels: {
        finish: "Submit"
    },
    onStepChanging: function (event, currentIndex, newIndex) {
        return currentIndex > newIndex || !(3 === newIndex && Number($("#age-2").val()) < 18) && (currentIndex < newIndex && (form.find(".body:eq(" + newIndex + ") label.error").remove(), form.find(".body:eq(" + newIndex + ") .error").removeClass("error")), form.validate().settings.ignore = ":disabled,:hidden", form.valid())
    },
    onFinishing: function (event, currentIndex) {
        return form.validate().settings.ignore = ":disabled", form.valid()
    },
    onFinished: function (event, currentIndex) {
        var StartupType = [];
        var StartupTechnology = [];
        var SellType = [];
        var ProductCharge = [];
        var MoneyRaisePlan = [];
        // var IdeaProgress = [];

        $.each($("input[name='StartupType']:checked"), function () {
            StartupType.push($(this).val());
        });
        $.each($("input[name='StartupTechnology']:checked"), function () {
            StartupTechnology.push($(this).val());
        });
        $.each($("input[name='SellType']:checked"), function () {
            SellType.push($(this).val());
        });
        $.each($("input[name='ProductCharge']:checked"), function () {
            ProductCharge.push($(this).val());
        });
        $.each($("input[name='MoneyRaisePlan']:checked"), function () {
            MoneyRaisePlan.push($(this).val());
        });

        //// $.each($("textarea.progress"), function () {

        //         IdeaProgress.push($(this).attr("name"));

        // });



        var IdeaModel = {
            "AboutYou": { "Entrepreneur": $.trim($("#Entrepreneur").val()), "YearsDoing": $.trim($("#YearsDoing").val()), "Experience": $.trim($("#Experience").val()), "Priorities": $.trim($("#Priorities").val()), "EndGoal": $.trim($("#EndGoal").val()) },
            "Company": { "CompanySetup": $.trim($("#CompanySetup").val()), "CompanyName": $.trim($("#CompanyName").val()), "HaveGotDomain": $.trim($("#HaveGotDomain").val()), "DomainName": $.trim($("#DomainName").val()), "Cofounder": $.trim($("#Cofounder").val()), "SupportTechnically": $.trim($("#SupportTechnically").val()), "BuildFrom": $.trim($("#BuildFrom").val()), "BrandThought": $.trim($("#BrandThought").val()), "CompanyMission": $.trim($("#CompanyMission").val()), "CompanyLookFeel": $.trim($("#CompanyLookFeel").val()) },
            "IdeaSelling": { "SellType": SellType.join(","), "ProductBuy": $.trim($("#ProductBuy").val()), "ProductCharge": ProductCharge.join(","), "ChargeGoing": $.trim($("#ChargeGoing").val()), "SellTo": $.trim($("#SellTo").val()), "CustomerFindPlan": $.trim($("#CustomerFindPlan").val()), "SaleStaffPlan": $.trim($("#SaleStaffPlan").val()) },
            "Money": { "BusinessCost": $.trim($("#BusinessCost").val()), "Affort": $.trim($("#Affort").val()), "MoneyRaisePlan": MoneyRaisePlan.join(","), "ProfitableMake": $.trim($("#ProfitableMake").val()), "ProfitableThinkTime": $.trim($("#ProfitableThinkTime").val()) },
            "IdeaBreakDown": { "StartupType": StartupType.join(","), "StartupTechnology": StartupTechnology.join(","), "ProblemSolve": $.trim($("#ProblemSolve").val()), "ProblemSolver": $.trim($("#ProblemSolver").val()), "FeedBackReceived": $.trim($("#FeedBackReceived").val()), "FeedBackFrom": $.trim($("#FeedBackFrom").val()), "ProductDemand": $.trim($("#ProductDemand").val()), "Niche": $.trim($("#Niche").val()), "InMarketAlready": $.trim($("#InMarketAlready").val()), "SpaceExist": $.trim($("#SpaceExist").val()), "Scalable": $.trim($("#Scalable").val()) },
            "IdeaID": $.trim($("#IdeaID").val()),
            "IdeaExplain": $.trim($("#IdeaExplain").val()),
            "ProgressValue": $.trim($("#ProgressValue").val())
        };

        var ActionType = $("#ActionType").val();
        var url = GetBaseURL() + "Idea/AddNewIdea"
        if (ActionType == "UPDATE")
            url = GetBaseURL() + "Idea/UpdateIdea"

        $.ajax({
            url: url,
            type: "POST",
            data: JSON.stringify(IdeaModel),
            contentType: "application/json",
            dataType: "json",

            error: function (response) {
                //if (ActionType == "UPDATE")
                //    swal({
                //        title: "Success!",
                //        text: "Idea Submitted Successfuly!",
                //        icon: "success",
                //        button: "Close!",
                //    });


                //else

                //    swal({
                //        title: "Success!",
                //        text: "Idea Updated Successfuly!",
                //        icon: "success",
                //        button: "Close!",
                //    });

                setTimeout(function () { window.location.replace(GetBaseURL() + "Idea"); }, 1000);
            },
            success: function (response) {
                alert(response);
            }
        });







        console.log($.parseJSON($("form").serialize()));
        console.log($.parseJSON($('form').serializeArray()));
        // var data = {};
        // $("form").serializeArray().map(function (x) { data[x.name] = x.value; })
        //console.log(data);
        Swal.fire("Form Submitted!", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed lorem erat eleifend ex semper, lobortis purus sed.");
    }
}), $(".validation-wizard").validate({
    ignore: "input[type=hidden]",
    errorClass: "text-danger",
    successClass: "text-success",
    highlight: function (element, errorClass) {
        $(element).removeClass(errorClass)
    },
    unhighlight: function (element, errorClass) {
        $(element).removeClass(errorClass)
    },
    errorPlacement: function (error, element) {
        error.insertAfter(element)
    },
    rules: {
        email: {
            email: !0
        }
    }
});

$("li.disabled").css("display", "block !important");
