// Flot Charts sample data for SB Admin template

// Flot Line Chart with Tooltips
$(document).ready(function() {
    console.log("document ready");
    var offset = 0;
    plot();

    function plot() {
        var sin = [],
            cos = [];
        for (var i = 0; i < 12; i += 0.2) {
            sin.push([i, Math.sin(i + offset)]);
            cos.push([i, Math.cos(i + offset)]);
        }

        var options = {
            series: {
                lines: {
                    show: true
                },
                points: {
                    show: true
                }
            },
            grid: {
                hoverable: true //IMPORTANT! this is needed for tooltip to work
            },
            yaxis: {
                min: -1.2,
                max: 1.2
            },
            tooltip: true,
            tooltipOpts: {
                content: "'%s' of %x.1 is %y.4",
                shifts: {
                    x: -60,
                    y: 25
                }
            }
        };

        
    }
});

// Flot Pie Chart with Tooltips
$(function() {

    var data = [{
        label: "Word 1",
        data: 25,
        color: "red"
    }, {
        label: "Word 2",
        data: 25,
        color: "orange"
    }, {
        label: "Word 3",
        data: 25,
        color: "yellow"
    }, {
        label: "Word 4",
        data: 25,
        color: "green"
    }, {
        label: "Word 5",
        data: 25,
        color: "greenyellow"
    }, {
        label: "Word 6",
        data: 25,
        color: "blue"
    }, {
        label: "Word 7",
        data: 25,
        color: "aqua"
    }, {
        label: "Word 8",
        data: 25,
        color: "purple"
    }, {
        label: "Word 9",
        data: 25,
        color: "pink"
    }, {
        label: "Word 10",
        data: 25,
        color: "darkred"
    }];

    var plotObj = $.plot($("#flot-pie-chart"), data, {
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

});