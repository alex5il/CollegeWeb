$(document).ready(ajax_call);

function games_display(games) {
    $('#fountainG').toggle();
    $.each(games.results, function (i, result) {
        $('#games').append('<h2>Title</h2><h3>' + result.name + '</h3>' + result.description);
    });
}

function ajax_call() {
    $.ajax({
        url: "http://www.giantbomb.com/api/games/",
        type: "get",
        data: { api_key: "e31a53182866e975585bd1891e5716df2864eebf", field_list: "description,name", limit: "1", offset: offset, format: "jsonp", sort: "number_of_user_reviews:desc", platforms: "94", filter: "description:null", json_callback: "games_display" },
        dataType: "jsonp"
    });
}