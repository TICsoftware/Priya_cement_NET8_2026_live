document.addEventListener("DOMContentLoaded", () => {
  const wrapper = document.querySelector(".map-outer-warapper");
  if (!wrapper) return;

  const states = wrapper.querySelectorAll(".map-state");
  const items = wrapper.querySelectorAll(".map-item");
  const cards = wrapper.querySelectorAll(".map-info-card");

  function getItemByLocation(location) {
    return Array.from(items).find(
      (item) => item.querySelector(".map-hover")?.getAttribute("data-location") === location
    );
  }

  function getStateByLocation(location) {
    return Array.from(states).find(
      (state) => state.getAttribute("data-location") === location
    );
  }

  function hideCards() {
    cards.forEach((card) => {
      card.classList.remove("is-active");
      card.setAttribute("aria-hidden", "true");
    });
  }

  function showCard(location) {
    hideCards();
    const card = Array.from(cards).find(
      (el) => el.getAttribute("data-location") === location
    );
    if (!card) return;
    card.classList.add("is-active");
    card.setAttribute("aria-hidden", "false");
  }

  function clearAllActive() {
    states.forEach((s) => s.classList.remove("active"));
    items.forEach((i) => i.classList.remove("active", "linked-active"));
    wrapper.classList.remove("has-active");
    hideCards();
  }

  function activateLocation(location) {
    if (!location) return;
    clearAllActive();

    const state = getStateByLocation(location);
    const item = getItemByLocation(location);

    if (state) state.classList.add("active");
    if (item) item.classList.add("linked-active");
    wrapper.classList.add("has-active");
    showCard(location);
  }

  items.forEach((item) => {
    const circle = item.querySelector(".map-hover");
    const line = item.querySelector(".map-line");
    const labelGroup = item.querySelector(".map-label-group");
    const labelBg = item.querySelector(".map-label-bg");
    const labelText = item.querySelector(".map-label");

    if (!circle || !line || !labelGroup || !labelBg || !labelText) return;

    const cx = parseFloat(circle.getAttribute("cx"));
    const cy = parseFloat(circle.getAttribute("cy"));
    const lx = parseFloat(item.getAttribute("data-lx")) || cx;
    const ly = parseFloat(item.getAttribute("data-ly")) || cy - 40;
    const textLength = labelText.getComputedTextLength();
    const paddingX = 16;
    const minWidth = 40;
    const labelHeight = parseFloat(labelBg.getAttribute("height")) || 25;
    const labelWidth = Math.max(textLength + paddingX, minWidth);
    const isRightSide = lx >= cx;
    const boxX = isRightSide ? lx : lx - labelWidth;

    line.setAttribute("d", `M${cx},${cy} L${lx},${ly}`);
    labelGroup.setAttribute("transform", `translate(${boxX}, ${ly - labelHeight / 2})`);
    labelBg.setAttribute("x", 0);
    labelBg.setAttribute("y", 0);
    labelBg.setAttribute("width", labelWidth);
    labelBg.setAttribute("height", labelHeight);
    labelText.setAttribute("x", labelWidth / 2);
    labelText.setAttribute("y", labelHeight / 2);
  });

  items.forEach((item) => {
    const circle = item.querySelector(".map-hover");
    const location = circle?.getAttribute("data-location");
    if (!location) return;

    item.addEventListener("mouseenter", () => {
      const state = getStateByLocation(location);
      if (state) state.classList.add("hover-linked");
    });

    item.addEventListener("mouseleave", () => {
      const state = getStateByLocation(location);
      if (state) state.classList.remove("hover-linked");
    });
  });

  states.forEach((state) => {
    const location = state.getAttribute("data-location");

    state.addEventListener("mouseenter", () => {
      const item = getItemByLocation(location);
      if (item) item.classList.add("linked-hover");
    });

    state.addEventListener("mouseleave", () => {
      const item = getItemByLocation(location);
      if (item) item.classList.remove("linked-hover");
    });

    state.addEventListener("click", () => {
      const isAlreadyActive = state.classList.contains("active");
      if (isAlreadyActive) {
        clearAllActive();
      } else {
        activateLocation(location);
      }
    });
  });

  items.forEach((item) => {
    const circle = item.querySelector(".map-hover");
    if (!circle) return;

    circle.addEventListener("click", () => {
      const location = circle.getAttribute("data-location");
      const isAlreadyActive = item.classList.contains("linked-active");
      if (isAlreadyActive) {
        clearAllActive();
      } else {
        activateLocation(location);
      }
    });
  });
});
