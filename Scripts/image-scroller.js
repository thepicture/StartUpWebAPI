"use strict"

const SCROLL_INTERVAL_MS = 5 * 1000;
let interval;

    btnGoLeft.addEventListener('mousedown', scrollPicturesToLeft);
btnGoRight.addEventListener('mousedown', scrollPicturesToRight);

function scrollPicturesToLeft() {
    resetIntervalToPreventNonIntendedScroll();
    imageBoard.style.transform = `translateX(${(getCurrentTransform() + 200) % imageBoard.clientWidth}px)`;

    if (getCurrentTransform() > 0) {
        imageBoard.style.transform = `translateX(${-(imageBoard.clientWidth - 200)}px)`;
    }
}

function scrollPicturesToRight() {
    resetIntervalToPreventNonIntendedScroll();
    let transform = getCurrentTransform();

    imageBoard.style.transform = `translateX(${(transform - 200) % imageBoard.clientWidth}px)`;
}

function getCurrentTransform() {
    if (imageBoard.style.transform.length === 0) {
        imageBoard.style.transform = "translateX(0px)";
    }

    let result = parseInt(imageBoard
        .style
        .transform
        .split('(')[1]
        .split('px')[0]
    );

    return result;
}

function resetIntervalToPreventNonIntendedScroll() {
    clearInterval(interval);
    interval = setInterval(scrollPicturesToRight, SCROLL_INTERVAL_MS);
}

interval = setInterval(scrollPicturesToRight, SCROLL_INTERVAL_MS);