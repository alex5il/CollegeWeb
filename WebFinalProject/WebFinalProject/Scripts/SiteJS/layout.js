$(document).ready(function () {

    // logo
    function draw() {
        var canvas = document.getElementById("canvas");
        if (canvas.getContext) {
            var ctx = canvas.getContext("2d");

            ctx.font = "28px Comic Sans MS";
            ctx.fillStyle = "#B5D6C0";
            ctx.fillText("Giga games", 0, 20);
        }
    }

    draw();

    $('.carousel').carousel({
        interval: 3000
    });
});