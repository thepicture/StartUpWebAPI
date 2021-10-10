"use strict"

const TIMEOUT_MS = 1000;

function removeReason() {
    const reason = document.querySelector('.reason');

    setTimeout(() => reason.remove(), TIMEOUT_MS);
}

document.addEventListener('DOMContentLoaded', () => removeReason.bind(this));