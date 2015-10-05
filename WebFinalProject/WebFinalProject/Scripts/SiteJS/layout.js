$(document).ready(function () {

    // logo
    function draw() {
        var canvas = document.getElementById("canvas");
        if (canvas.getContext) {
            var ctx = canvas.getContext("2d");

            ctx.font = "28px Comic Sans MS";
            ctx.fillStyle = "#C8C8C8";
            ctx.fillText("<Giga games>", 0, 20);
        }

        // canvas event listeners

        // Start animating when the mouse enters the canvas
        canvas.addEventListener('mouseover', function (e) {
            redraw("#69A8DE", true);
        });

        // Stop animating when the mouse exits the canvas
        canvas.addEventListener('mouseout', function (e) {
            redraw("#C8C8C8");
        });

        function redraw(color, isOver) {
            ctx.save();
            ctx.clearRect(0, 0, canvas.width, canvas.height);

            ctx.font = "28px Comic Sans MS";
            ctx.fillStyle = color;
            ctx.fillText("<Giga games>", 0, 20);

            ctx.restore();
        }
    }

    draw();

    $('.carousel').carousel({
        interval: 3000
    });
});