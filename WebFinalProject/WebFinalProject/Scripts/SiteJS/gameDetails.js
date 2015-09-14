$(document).ready(function () {

    var pie = new d3pie("myPie", {
        header: {
            title: {
                text: "Game reviews:"
            }
        },
        data: {
            content: [
                { label: "Bad", value: +badReview, color: "red" },
                { label: "Good", value: +goodReview, color: "orange" },
                { label: "Great", value: +greatReview, color: "green" },
            ]
        }
    });
});