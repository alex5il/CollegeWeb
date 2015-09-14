$(document).ready(function() {
    // We wire the event on document since we need to re-bind the event to every a-tag within the contentPager
    var ajaxFormSubmit = function () {
        var $form = $(this);

        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };

        $.ajax(options).done(function (data) {
            var $target = $($form.data("otf-target"));
            var $newHtml = $(data);

            replaceData($target, $newHtml);
        });

        return false;
    };

    var getPage = function () {
        var $a = $(this);

        var options = {
            url: $a.attr("href"),
            data: $("form").serialize(),
            type: "get"
        };

        $.ajax(options).done(function (data) {
            var $target = $($a.parents("div.pagedList").data("otf-target"));

            replaceData($target, $(data));
        });
        return false;
    };

    function replaceData(target, data) {
        target.empty();
        target.append(data);
    }

    $("form[data-otf-ajax='true']").submit(ajaxFormSubmit);
    $(document).on("click", ".pagedList a", getPage);
});