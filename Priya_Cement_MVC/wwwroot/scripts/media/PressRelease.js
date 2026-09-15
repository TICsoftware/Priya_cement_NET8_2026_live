$(document).on("click", "#loadMoreBtn", function () {

    var button = $(this);

    var page = parseInt(button.attr("data-page")) + 1;
    var pageSize = parseInt(button.attr("data-pagesize"));
    var total = parseInt(button.attr("data-total"));
    var cont_id = parseInt(button.attr("data-contid"));

    button.prop("disabled", true).text("Loading...");

    $.ajax({
        url: '@Url.Action("LoadMorePressReleases", "Media")',
        type: 'GET',
        data: {
            cont_id: cont_id,
            pageSize: pageSize,
            pageNumber: page
        },

        success: function (result) {

            $("#pressReleaseList").append(result);

            button.attr("data-page", page);

            var loaded = page * pageSize;

            if (loaded >= total) {
                button.remove();
            }
            else {
                button.prop("disabled", false).text("Load More");
            }
        },

        error: function () {
            button.prop("disabled", false).text("Load More");
            alert("Unable to load more records.");
        }
    });
});