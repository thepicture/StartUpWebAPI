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
    const boxes = document.querySelectorAll('.template-live');

    for (let box of boxes) {
        box.remove();
    }

    isNeededToShowDescription = false;
}

function drawDescription(className, descriptionText) {
    const startUp = document.querySelector("." + className);

    const description = document.querySelector('#description-template').cloneNode(true);
    description.querySelector('#description-text').innerText = descriptionText;
    description.classList.add('template-live');
    description.style = "display:flex;";
    description.style.left = startUp.offsetLeft + "px";
    description.style.top = startUp.offsetTop + "px";
    startUp.append(description);
}