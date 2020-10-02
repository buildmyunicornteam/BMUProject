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