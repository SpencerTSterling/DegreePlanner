
// Function to redirect to course detail
function goToCourseDetail(courseId) {
    window.location.href = '/Course/Detail/' + courseId;
}

// Function to mark course item as complete
function markAsComplete(itemId, checkbox) {
    console.log('Checkbox clicked for item ID:', itemId, 'Completed:', checkbox.checked);
    var isCompleted = checkbox.checked;

    $.post('/Dashboard/MarkCourseItemAsComplete', { id: itemId, isCompleted: isCompleted })
        .done(function () {
            if (isCompleted) {
                $(checkbox).closest('.course-item').addClass('list-group-item-success');
            } else {
                $(checkbox).closest('.course-item').removeClass('list-group-item-success');
            }
        })
        .fail(function () {
            console.error('Failed to update course item.');
        });
}

// Function to perform search
function performSearch(query) {
    if (query.length < 3) {
        $('#searchResults').html(''); // Clear if less than 3 characters
        return;
    }

    $.ajax({
        url: '/Dashboard/Search',
        type: 'GET',
        data: { query: query },
        success: function (data) {
            let resultsHtml = '';

            if (data.Terms.length) {
                resultsHtml += '<h5>Terms</h5>';
                data.Terms.forEach(term => {
                    resultsHtml += `<div class="search-item" onclick="redirectToDashboard(${term.id})">${term.name}</div>`;
                });
            }

            if (data.Courses.length) {
                resultsHtml += '<h5>Courses</h5>';
                data.Courses.forEach(course => {
                    resultsHtml += `<div class="search-item" onclick="redirectToCourse(${course.id})">${course.name}</div>`;
                });
            }

            if (data.CourseItems.length) {
                resultsHtml += '<h5>Course Items</h5>';
                data.CourseItems.forEach(item => {
                    resultsHtml += `<div class="search-item" onclick="redirectToCourse(${item.courseId})">${item.name}</div>`;
                });
            }

            $('#searchResults').html(resultsHtml);
        },
        error: function () {
            console.error('Search failed.');
        }
    });
}

// Redirect functions
function redirectToDashboard(termId) {
    window.location.href = `/Dashboard/Index?selectedTermId=${termId}`;
}

function redirectToCourse(courseId) {
    window.location.href = `/Course/Detail/${courseId}`;
}

// Document Ready Function
$(document).ready(function () {
    // Load courses and course items on term change
    $('#termId').change(function () {
        var termId = $(this).val();
        if (termId !== '') {
            $.get('/Dashboard/GetCoursesByTerm', { termId: termId })
                .done(function (data) {
                    $('#courseContainer').html(data);
                })
                .fail(function () {
                    console.error('Failed to load courses.');
                });

            $.get('/Dashboard/GetCourseItemsByTerm', { termId: termId })
                .done(function (data) {
                    $('#courseItems').html(data);
                })
                .fail(function () {
                    console.error('Failed to load course items.');
                });
        } else {
            $('#courseContainer').empty();
            $('#courseItems').empty();
        }
    });

    // Trigger change event to load default term's courses on page load
    $('#termId').trigger('change');

    // Bind search box input event
    $('#searchBox').on('input', function () {
        performSearch($(this).val());
    });
});
