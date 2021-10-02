"use strict"

// Events
$('.dropdown-container')
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
    .on('change', '[type="checkbox"]', function () {
        var container = $(this).closest('.dropdown-container');
        var numChecked = container.find('[type="checkbox"]:checked').length;
        container.find('.quantity').text(numChecked || 'Любой регион');
    });