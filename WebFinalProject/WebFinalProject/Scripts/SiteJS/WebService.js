$(document).ready(function () {
    $.ajax({
        url: "http://www.giantbomb.com/api/games/",
        type: "get",
        data: { api_key: "e31a53182866e975585bd1891e5716df2864eebf", field_list: "description,name", limit: "1", format: "jsonp", sort: "number_of_user_reviews:desc", platforms: "94", filter: "description:null", json_callback: "games_display" },
        dataType: "jsonp"
    });
});

function games_display(games) {
    console.log(games);
    $.each(games.results, function (i, result) {
        if (result.description != "") {
            $('#games').append('<h2>Title</h2><h3>' + result.name + '</h3>' + result.description);
        }
    });
}