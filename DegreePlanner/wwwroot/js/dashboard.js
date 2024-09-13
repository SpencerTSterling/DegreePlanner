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
            // Log the data to ensure it's being returned correctly
            console.log('Data from search:', data);

            let resultsHtml = '';

            // Safely process terms (check if data.terms is defined and has content)
            if (data.terms && Array.isArray(data.terms) && data.terms.length > 0) {
                resultsHtml += '<h5>Terms</h5>';
                data.terms.forEach(term => {
                    resultsHtml += `<div class="search-item" onclick="redirectToDashboard(${term.id})">${term.name}</div>`;
                });
            }

            // Safely process courses (check if data.courses is defined and has content)
            if (data.courses && Array.isArray(data.courses) && data.courses.length > 0) {
                resultsHtml += '<h5>Courses</h5>';
                data.courses.forEach(course => {
                    resultsHtml += `<div class="search-item" onclick="redirectToCourse(${course.id})">${course.name}</div>`;
                });
            }

            // Safely process course items (check if data.courseItems is defined and has content)
            if (data.courseItems && Array.isArray(data.courseItems) && data.courseItems.length > 0) {
                resultsHtml += '<h5>Course Items</h5>';
                data.courseItems.forEach(item => {
                    resultsHtml += `<div class="search-item" onclick="redirectToCourse(${item.courseId})">${item.name}</div>`;
                });
            }

            // If no results
            if (!resultsHtml) {
                resultsHtml = '<p>No results found.</p>';
            }


            $('#searchResults').html(resultsHtml);  // Update the HTML content of the search results
        },
        error: function (xhr, status, error) {
            console.error('Search failed: ', status, error);
            $('#searchResults').html('<p>Search failed. Please try again.</p>');
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
    //$('#termId').trigger('change');

    // Bind search box input event
    $('#searchBox').on('input', function () {
        performSearch($(this).val());
    });
    // Check if there's a selectedTermId query parameter and set it in the drop-down
    var urlParams = new URLSearchParams(window.location.search);
    var selectedTermId = urlParams.get('selectedTermId');
    if (selectedTermId) {
        $('#termId').val(selectedTermId).trigger('change');
    } else {
        // Trigger change event to load default term's courses on page load
        $('#termId').trigger('change');
    }

});
