$(document).on("click", ".btnProcess", function () {

    var processName = $(this).attr("data-process");

    var $article = $('.manufacturing-outer-grid article[data-process]')
        .filter(function () {
            return $(this).attr("data-process")?.trim().toLowerCase()
                === processName?.trim().toLowerCase();
        })
        .first();

    if ($article.length) {
        $('html, body').animate({
            scrollTop: $article.offset().top - 100
        }, 500);
    } else {
        console.log("No matching article found:", processName);
    }
});