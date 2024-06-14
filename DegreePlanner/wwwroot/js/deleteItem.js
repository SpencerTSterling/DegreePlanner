document.addEventListener('DOMContentLoaded', function () {
    const deleteButtons = document.querySelectorAll('.delete-button');

    deleteButtons.forEach(button => {
        button.addEventListener('click', function () {
            const itemId = this.getAttribute('data-id');
            const deleteUrl = this.getAttribute('data-url');

            if (confirm('Are you sure you want to delete this item?')) {
                fetch(deleteUrl, {
                    method: 'DELETE',
                    headers: {
                        'X-CSRF-TOKEN': document.querySelector('input[name="__RequestVerificationToken"]').value
                    }
                })
                    .then(response => response.json())
                    .then(data => {
                        if (data.success) {
                            alert('Item deleted successfully.');
                            window.location.reload();
                        } else {
                            alert('Error deleting item.');
                        }
                    })
                    .catch(error => console.error('Error:', error));
            }
        });
    });
});
