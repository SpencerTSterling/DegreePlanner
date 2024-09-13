
// _CourseCards functions
    // A helper function to strip HTML tags (such as <p> and <br>)
    public static string StripHtmlTags(string input)
{
    if (string.IsNullOrEmpty(input))
        return input;

    // Strips out <p>, <br>, and other tags
    return System.Text.RegularExpressions.Regex.Replace(input, "<.*?>", string.Empty);
}



// _CourseItemsList functions
function goToCourseDetail(courseId) {
    // Redirect to the course detail page
    window.location.href = '/Course/Detail/' + courseId;
}

function markAsComplete(itemId, checkbox) {
    console.log('Checkbox clicked for item ID:', itemId, 'Completed:', checkbox.checked);
    var isCompleted = checkbox.checked;

    $.post('@Url.Action("MarkCourseItemAsComplete", "Dashboard")', { id: itemId, isCompleted: isCompleted })
        .done(function () {
            // Update the UI based on the completion state without refreshing
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


// Index page functions
$(document).ready(function () {
    $('#termId').change(function () {
        var termId = $(this).val();
        if (termId !== '') {
            $.get('@Url.Action("GetCoursesByTerm", "Dashboard")', { termId: termId })
                .done(function (data) {
                    $('#courseContainer').html(data);
                })
                .fail(function () {
                    console.error('Failed to load courses.');
                });

            $.get('@Url.Action("GetCourseItemsByTerm", "Dashboard")', { termId: termId })
                .done(function (data) {
                    $('#courseItems').html(data);
                })
                .fail(function () {
                    console.error('Failed to load course items.');
                });
        } else {
            $('#courseContainer').empty();
            $('#courseItems').empty()
        }
    });

    // Trigger change event to load default term's courses on page load
    $('#termId').trigger('change');
});

function viewCourseDetails(courseId) {
    window.location.href = '@Url.Action("Detail", "Course")' + '/' + courseId;
}