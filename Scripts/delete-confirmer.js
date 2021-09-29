"use strict"

function getDeleteItemState(itemName) {
    const userWantsToDeleteItem = confirm("Точно удалить " + itemName + "? Действие нельзя отменить", "");

    if (userWantsToDeleteItem) {
        return true;
    } else {
        return false;
    }
}