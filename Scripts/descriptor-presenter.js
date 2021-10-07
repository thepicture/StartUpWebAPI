"use strict"

const MILLISECONDS_TO_WAIT = 50;
let isNeededToShowDescription = false;
let currentTimeout;

function showDescription(className, descriptionText) {
    if (!descriptionText) return;

    isNeededToShowDescription = true;

    if (currentTimeout) clearTimeout(currentTimeout);

    currentTimeout = setTimeout(() => {
        if (isNeededToShowDescription) {
            drawDescription(className, descriptionText);
        }
    }, MILLISECONDS_TO_WAIT);
}

function hideDescription() {
    const boxes = document.querySelectorAll('.description');

    for (let box of boxes) {
        box.remove();
    }
    isNeededToShowDescription = false;
}

function drawDescription(className, descriptionText) {
    const startUp = document.querySelector("." + className);

    const description = document.createElement('div');
    description.classList.add('tag-item', 'description');
    description.innerText = descriptionText;
    description.style.left = startUp.offsetLeft + "px";
    description.style.top = startUp.offsetTop - 100 - 45 + "px";
    startUp.append(description);
}