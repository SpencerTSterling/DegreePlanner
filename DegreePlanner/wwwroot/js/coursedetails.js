
function markAsComplete(itemId, checkbox) {
    console.log('Checkbox clicked for item ID:', itemId, 'Completed:', checkbox.checked);
    var isCompleted = checkbox.checked;

    $.post('/Course/MarkCourseItemAsComplete', { id: itemId, isCompleted: isCompleted })
        .done(function () {
            if (isCompleted) {
                $(checkbox).closest('.list-group-item').addClass('list-group-item-success');
            } else {
                $(checkbox).closest('.list-group-item').removeClass('list-group-item-success');
            }
        })
        .fail(function () {
            console.error('Failed to update course item.');
        });
}

$(document).ready(function () {
    // Additional logic for the Course Detail page can go here if needed.
});
