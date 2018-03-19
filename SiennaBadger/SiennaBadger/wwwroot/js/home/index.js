$("#ParseButton").click(function () {
    console.log("You clicked parse!");

    // TODO - client side validation
    var parseUrl = $("#ParseUrl").val();
    Search(parseUrl);
});

function Search(parseUrl) {
    //$("#job_details_div").html("Loading...");

    // call api to scrape web page
    $.post('/api/v1.0/page/parse',
        {
            parseUrl: parseUrl
        },

        // TODO handle errors, check nulls
        function(data) {
            console.log(data);

            $("#SummaryTitle").html("Summary - " + parseUrl);
            $("#WordCount").html(data.wordCount);
            $("#ImageCount").html(data.images.length);

            UpdateWordsTable(data.words);
            UpdateWordsChart(data.words);
            UpdateImageGallery(data.images);
        });
}
function UpdateWordsTable(data) {

    console.log(data);
    var tableHTML = '';
    for (var i = 0; i < data.length; i++) {
        tableHTML += "<tr><td>" + data[i].text + "</td><td>" + data[i].count + "</td></tr>";
    }

    $("#WordsTable tbody").html(tableHTML);

}

function UpdateWordsChart(data) {

    console.log(data);
    var chartData = [];
    var colors = ["red", "orange", "yellow", "green", "greenyellow", "blue", "aqua", "purple", "pink", "darkred"];
    for (var i = 0; i < data.length; i++) {
        chartData.push({label: data[i].text, data: data[i].count, color: colors[i]});
    }

    var plotObj = $.plot($("#flot-pie-chart"), chartData, {
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

function UpdateImageGallery(data) {

    console.log(data);
    var galleryHmtl = '';
    for (var i = 0; i < data.length; i++) {
        galleryHmtl += "<div class=\"col-lg-3 col-md-4 col-xs-6 thumb\"><a target=\"_blank\" class=\"thumbnail\" href=\"" + data[i].url + "\"><img class=\"img- responsive\" src=\"" + data[i].url + "\" alt=\"\"></a><small>imagename.jpg</small><p class=\"help- block small\">400x120</p></div>";
    }

    $("#ImageGallery .panel-body").html(galleryHmtl);     
}