"use strict"

function flushDropDownBox(selector) {
    const dropDownBox = document.querySelector(selector);

    if (!dropDownBox) return;

    const checkBoxes = dropDownBox.querySelectorAll('input[type="checkbox"]');

    Array.from(checkBoxes).forEach(c => c.checked = false);

    alert("Значения успешно сброшены!");

    return false;
}