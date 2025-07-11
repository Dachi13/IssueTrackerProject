$(document).ready(function () {

    var ticketIdToDelete;

    $('.delete-ticket-btn').on('click', function () {
        ticketIdToDelete = $(this).data('ticket-id');
    });

    $('#confirmDeleteBtn').on('click', function () {
        $.ajax({
            url: '/api/tickets/' + ticketIdToDelete,
            method: 'DELETE',
            success: function () {
                $('#deleteConfirmModal').modal('hide');

                $('button[data-ticket-id="' + ticketIdToDelete + '"]').closest('tr').fadeOut(500, function () {
                    $(this).remove();
                });
            },
            error: function () {
                alert('Error: Could not delete the ticket.');
                $('#deleteConfirmModal').modal('hide');

            }
        });
    });
});