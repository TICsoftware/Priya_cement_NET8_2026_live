$(document).on("click", ".btnProcess", function () {

    var processName = $(this).data("process");

    var $slide = $('.second-slider .swiper-slide')
        .filter(function () {
            return $(this).data("process") === processName;
        })
        .first();

    if ($slide.length) {
        var index = $slide.index();
        secondSlider.slideTo(index);
    }
});