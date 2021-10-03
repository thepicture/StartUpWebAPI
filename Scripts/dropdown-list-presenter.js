"use strict"

$('#members-container')
    .on('click', '.dropdown-button', function () {
        $(this).siblings('.dropdown-list').toggle();
    })
    .on('input', '.dropdown-search', function () {
        var target = $(this);
        var dropdownList = target.closest('.dropdown-list');
        var search = target.val().toLowerCase();

        if (!search) {
            dropdownList.find('li').show();
            return false;
        }

        dropdownList.find('li').each(function () {
            var text = $(this).text().toLowerCase();
            var match = text.indexOf(search) > -1;
            $(this).toggle(match);
        });
    })
    .on('change', '[type="checkbox"]', insertMembersNumber);

$('#regions-container')
    .on('click', '.dropdown-button', function () {
        $(this).siblings('.dropdown-list').toggle();
    })
    .on('input', '.dropdown-search', function () {
        var target = $(this);
        var dropdownList = target.closest('.dropdown-list');
        var search = target.val().toLowerCase();

        if (!search) {
            dropdownList.find('li').show();
            return false;
        }

        dropdownList.find('li').each(function () {
            var text = $(this).text().toLowerCase();
            var match = text.indexOf(search) > -1;
            $(this).toggle(match);
        });
    })
    .on('change', '[type="checkbox"]', insertRegionsNumber);

$('#categories-container')
    .on('click', '.dropdown-button', function () {
        $(this).siblings('.dropdown-list').toggle();
    })
    .on('input', '.dropdown-search', function () {
        var target = $(this);
        var dropdownList = target.closest('.dropdown-list');
        var search = target.val().toLowerCase();

        if (!search) {
            dropdownList.find('li').show();
            return false;
        }

        dropdownList.find('li').each(function () {
            var text = $(this).text().toLowerCase();
            var match = text.indexOf(search) > -1;
            $(this).toggle(match);
        });
    })
    .on('change', '[type="checkbox"]', insertCategoriesNumber);

function insertMembersNumber() {
    var container = $(this).closest('#members-container');
    var numChecked = container.find('[type="checkbox"]:checked').length;
    container.find('.quantity').text(numChecked || 'Любое');
}

function insertRegionsNumber() {
    var container = $(this).closest('#regions-container');
    var numChecked = container.find('[type="checkbox"]:checked').length;
    container.find('.quantity').text(numChecked || 'Любой');
}

function insertCategoriesNumber() {
    var container = $(this).closest('#categories-container');
    var numChecked = container.find('[type="checkbox"]:checked').length;
    container.find('.quantity').text(numChecked || 'Любые');
}

$('#members-container').ready(insertMembersNumber.bind($('#members-container')));
$('#regions-container').ready(insertRegionsNumber.bind($('#regions-container')));
$('#categories-container').ready(insertCategoriesNumber.bind($('#categories-container')));