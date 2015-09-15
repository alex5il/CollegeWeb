$(document).ready(function () {

    var pie = new d3pie("myPie", {
        header: {
            title: {
                text: "Game reviews:"
            }
        },
        data: {
            content: [
                { label: "Bad", value: +badReview, color: "#bf0000" },
                { label: "Good", value: +goodReview, color: "#CC6633" },
                { label: "Great", value: +greatReview, color: "#2B7558" },
            ]
        }, size: {
            canvasHeight: 350,
            canvasWidth: 350,
            pieInnerRadius: 12,
            pieOuterRadius: null
        },
    });

    // Dynamic thumb by value of the score
    var score = (+$(".game-score").text()) || 0;
    if (score > 60) {
        // add thumb up
        $(".review-icon").addClass("glyphicon glyphicon-thumbs-up");
    } else {
        // add thumb down
        $(".review-icon").addClass("glyphicon glyphicon-thumbs-down");
    }
});