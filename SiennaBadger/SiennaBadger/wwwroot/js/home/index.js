// simple namespacing
var wahoo = wahoo || {};

wahoo.Search = function (parseUrl) {
    //reset dashboard per request
    $("#ErrorContainer").hide();
    wahoo.ClearDashboard();

    // call api to scrape web page
    $.post("/api/v1.0/page/parse",
        {
            parseUrl: parseUrl
        },

        function(data) {
            console.log(data);
            if (data !== null) {
                $("#SummaryTitle").html("Summary - " + parseUrl);
                $("#WordCount").html(data.wordCount);
                $("#ImageCount").html(data.images.length);

                if (data.words.length > 0) {
                    wahoo.UpdateWordsTable(data.words);
                    wahoo.UpdateWordsChart(data.words);
                }
                if (data.images.length > 0) {
                    wahoo.UpdateImageGallery(data.images);
                }
            }
        })
        .fail(function (response) {
            console.log(response);
            $("#ErrorContainer").show();
            $("#ErrorMessage").text(JSON.parse(response.responseText));
        });
}
wahoo.UpdateWordsTable = function (data) {

    var tableHtml = "";
    for (var i = 0; i < data.length; i++) {
        tableHtml += "<tr><td>" + data[i].text + "</td><td>" + data[i].count + "</td></tr>";
    }

    $("#WordsTable tbody").html(tableHtml);

}

wahoo.UpdateWordsChart = function(data) {

    var chartData = [];
    //simple list of colors for the chart.
    var colors = ["red", "orange", "yellow", "green", "greenyellow", "blue", "aqua", "purple", "pink", "darkred"];

    // create chart data
    for (var i = 0; i < data.length; i++) {
        chartData.push({label: data[i].text, data: data[i].count, color: colors[i]});
    }

    // create pie chart
    $.plot($("#flot-pie-chart"), chartData, {
        series: {
            pie: {
                show: true
            }
        },
        grid: {
            hoverable: true
        },
        tooltip: true,
        tooltipOpts: {
            content: "%p.0%, %s", // show percentages, rounding to 2 decimal places
            shifts: {
                x: 20,
                y: 0
            },
            defaultTheme: false
        }
    });

}

wahoo.UpdateImageGallery = function(data) {

    var galleryHmtl = "";
    for (var i = 0; i < data.length; i++) {
        galleryHmtl += "<div class=\"col-lg-3 col-md-4 col-xs-6 thumb thumb-container\"><a target=\"_blank\" class=\"thumbnail\" href=\"" + data[i].url + "\"><img class=\"img- responsive\" src=\"" + data[i].url + "\" alt=\"\"></a></div>";
    }

    $("#ImageGallery .panel-body").html(galleryHmtl);     
}

wahoo.ClearDashboard = function () {
    $("#WordsTable tbody tr").remove();
    $("#ImageGallery .panel-body").html("");
    $("#flot-pie-chart").html("");
}

// simple url validation for now.
wahoo.IsUrl = function (url) {
    var regExp = /(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/;

    if (regExp.test(url)) {
        return true;
    } else {
        return false;
    }
}

// set event handlers
$(document).ready(function () {
   
    $("#ParseButton").click(function () {
        $("#ErrorContainer").hide();
        $("#ParseForm").removeClass("has-error");

        var parseUrl = $("#ParseUrl").val();
        
        // client side validation
        if (wahoo.IsUrl(parseUrl)) {
            wahoo.Search(parseUrl);
        } else {

            $("#ParseForm").addClass("has-error");
            $("#ErrorMessage").html("Please enter a valid url to target.");
            $("#ErrorContainer").show();
        }
    });
});
