"use strict"

const MILLISECONDS_TO_WAIT = 50; // Timeout in ms to show a description of the startup.
let isNeededToShow = false; // Checks if it is needed to show a description.
let currentTimeout;

// Shows the description.
function showDescription(className, descriptionText) {
    if (!descriptionText) return;

    isNeededToShow = true;

    if (currentTimeout) clearTimeout(currentTimeout);

    currentTimeout = setTimeout(() => {
        if (isNeededToShow) {
            drawDescription(className, descriptionText);
        }
    }, MILLISECONDS_TO_WAIT);
}

// Hides the description.
function hideDescription() {
    const boxes = document.querySelectorAll('.description');

    for (let box of boxes) {
        box.remove();
    }
    isNeededToShow = false;
}

// Draws the description.
function drawDescription(className, descriptionText) {
    const startUp = document.querySelector("." + className);

    const description = document.createElement('div');
    description.classList.add('tag-item', 'description');
    description.innerText = descriptionText;
    description.style.left = startUp.offsetLeft + "px";
    description.style.top = startUp.offsetTop - 100 - 45 + "px";
    startUp.append(description);
}