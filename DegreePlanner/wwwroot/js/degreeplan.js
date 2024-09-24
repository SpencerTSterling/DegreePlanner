function viewCourseDetails(courseId) {
    // Use the courseDetailsUrl variable that was defined in the Razor view
    $.get(courseDetailsUrl, { id: courseId })
        .done(function (data) {
            $('#courseDetailsModal .modal-body').html(data);
            $('#courseDetailsModal').modal('show');
        })
        .fail(function () {
            console.error('Failed to load course details.');
        });
}
$(document).ready(function () {
    // Handle the click event for term-header to toggle collapse
    $('.term-header').click(function (e) {
        // Only toggle collapse if not clicking on Edit/Delete icons
        var isIconClick = $(e.target).closest('.edit-term, .delete-term').length > 0;

        if (!isIconClick) {
            var target = $(this).attr('href');  // Get the collapse target from href attribute
            $(target).collapse('toggle');  // Toggle collapse

            // Toggle the caret icon
            var $caret = $(this).find('.term-caret');
            if ($caret.hasClass('bi-caret-down-fill')) {
                $caret.removeClass('bi-caret-down-fill').addClass('bi-caret-up-fill');
            } else {
                $caret.removeClass('bi-caret-up-fill').addClass('bi-caret-down-fill');
            }
        }
    });

    // Prevent collapse when clicking Edit/Delete icons
    $('.edit-term, .delete-term').click(function (e) {
        e.stopPropagation(); // Ensure the click event doesn't propagate to term-header
    });
});
