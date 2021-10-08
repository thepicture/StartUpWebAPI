"use strict"

btnGoLeft.addEventListener('mousedown', scrollPicturesToLeft);
btnGoRight.addEventListener('mousedown', scrollPicturesToRight);

function scrollPicturesToLeft() {
    imageBoard.style.transform = `translateX(${(getCurrentTransform() + 200) % imageBoard.clientWidth}px)`;

    if (getCurrentTransform() > 0) {
        imageBoard.style.transform = `translateX(${-(imageBoard.clientWidth - 200)}px)`;
    }
}

function scrollPicturesToRight() {
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