/* Horizontal table scroll hint. Fade shows while more columns sit off-screen. */
(() => {
  const initTableScrollFade = () => {
    const wraps = document.querySelectorAll('[data-table-scroll]');
    if (!wraps.length) return;

    const update = (wrap) => {
      const scroller = wrap.querySelector('.table-scroll-x');
      if (!scroller) return;
      const max = scroller.scrollWidth - scroller.clientWidth;
      const scrollable = max > 4;
      wrap.classList.toggle('is-scrollable', scrollable);
      wrap.classList.toggle('is-at-end', !scrollable || scroller.scrollLeft >= max - 4);
    };

    wraps.forEach((wrap) => {
      const scroller = wrap.querySelector('.table-scroll-x');
      if (!scroller) return;
      scroller.addEventListener('scroll', () => update(wrap), { passive: true });
      update(wrap);
    });

    window.addEventListener('resize', () => wraps.forEach(update), { passive: true });
  };

  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', initTableScrollFade);
  } else {
    initTableScrollFade();
  }
})();
