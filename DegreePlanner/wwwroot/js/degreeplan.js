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

    console.log("Document is ready!!!!!!!!!!!");
    $('.edit-term').on('click', function (e) {
        console.log('Edit button clicked');
        event.stopPropagation(); 
    });

    $('.delete-term').on('click', function (e) {
        console.log('Delete button clicked');
        event.stopPropagation(); 
    });

    // Caret toggle logic

    $('.term-header').click(function (e) {
        e.preventDefault(); // Prevent default behavior

        var target = $(this).attr('href');  // Get the target collapse ID
        $(target).collapse('toggle');  // Toggle collapse

        // Toggle the caret icon
        var $caret = $(this).find('.term-caret');
        if ($caret.hasClass('bi-caret-down-fill')) {
            $caret.removeClass('bi-caret-down-fill').addClass('bi-caret-up-fill');
        } else {
            $caret.removeClass('bi-caret-up-fill').addClass('bi-caret-down-fill');
        }
    });
    // Expand and collapse all logic
    $('#expandAll').click(function () {
        $('.collapse').collapse('show'); // Show all collapsed sections
        $('.term-caret').removeClass('bi-caret-down-fill').addClass('bi-caret-up-fill'); // Set all carets to up
    });

    $('#collapseAll').click(function () {
        $('.collapse').collapse('hide'); // Hide all collapsed sections
        $('.term-caret').removeClass('bi-caret-up-fill').addClass('bi-caret-down-fill'); // Set all carets to down
    });

});
