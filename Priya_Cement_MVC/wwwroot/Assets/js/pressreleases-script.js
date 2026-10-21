document.addEventListener("DOMContentLoaded", () => {
  if (typeof gsap === "undefined" || typeof ScrollTrigger === "undefined") return;
  if (window.matchMedia("(prefers-reduced-motion: reduce)").matches) return;

  if (typeof gsap.registerPlugin === "function") gsap.registerPlugin(ScrollTrigger);

  function getColCount(grid) {
    const w = window.innerWidth;
    if (grid && grid.classList.contains("image-gallery") && !grid.classList.contains("image-gallery-4")) {
      if (w > 1366) return 5;
      if (w > 1199) return 4;
      if (w > 991) return 3;
      if (w > 767) return 2;
      return 1;
    }
    if (w > 991) return 4;
    if (w > 575) return 2;
    return 1;
  }

  function riseCards(grid, cardSelector) {
    const cards = gsap.utils.toArray(grid.querySelectorAll(cardSelector)).filter(function (card) {
      return !card.dataset.riseReady;
    });
    if (!cards.length) return;

    grid.classList.add("is-rise-anim");

    cards.forEach((card) => {
      card.dataset.riseReady = "1";
      const index = Math.max(Array.prototype.indexOf.call(grid.children, card), 0);
      const col = index % getColCount(grid);
      const fromY = 60 * (index + 1);

      gsap.fromTo(
        card,
        { y: fromY, autoAlpha: 0, force3D: true },
        {
          y: 0,
          autoAlpha: 1,
          ease: "none",
          force3D: true,
          scrollTrigger: {
            trigger: card,
            start: `top ${90 - col * 4}%`,
            end: `top ${58 - col * 3}%`,
            scrub: 1,
            invalidateOnRefresh: true,
          },
        }
      );
    });
  }

  document.querySelectorAll(".fi-card-grid").forEach((grid) => {
    riseCards(grid, ".fi-report-card");
  });

  document.querySelectorAll(".print-section .image-gallery").forEach((grid) => {
    riseCards(grid, ".image-gallery-card, .press-release-text-card");
  });

  window.initPressReleaseRise = function () {
    document.querySelectorAll("#pressReleaseList").forEach((grid) => {
      riseCards(grid, ".image-gallery-card, .press-release-text-card");
    });
    if (typeof ScrollTrigger !== "undefined") ScrollTrigger.refresh();
  };
});
