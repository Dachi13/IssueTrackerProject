$(document).ready(function () {

    var ticketIdToDelete;

    $('.delete-ticket-btn').on('click', function () {
        ticketIdToDelete = $(this).data('ticket-id');
    });

    $('#confirmDeleteBtn').on('click', function () {
        $.ajax({
            url: 'delete/tickets/' + ticketIdToDelete,
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

$('.edit-ticket-btn').on('click', function () {
    var ticketId = $(this).data('ticket-id');

    $.ajax({
        url: 'get/ticket/' + ticketId,
        method: 'GET',
        success: function (ticket) {
            $('#edit-ticket-id').val(ticketId);
            $('#edit-title').val(ticket.title);
            $('#edit-description').val(ticket.description);
            $('#edit-priority').val(ticket.priority);
            $('#edit-status').val(ticket.status);
        },
        error: function () {
            alert('Error: Could not load ticket data')
        }
    })
})

$('#saveChangesBtn').on('click', function () {
    
    var ticketData = {
        Id: parseInt($('#edit-ticket-id').val()),
        Title: $('#edit-title').val(),
        Description: $('#edit-description').val(),
        Priority: parseInt($('#edit-priority').val()),
        Status: parseInt($('#edit-status').val())
    };

    $.ajax({
        url: 'edit/ticket',
        method: 'PUT',
        data: ticketData,
        success: function () {
            alert('Successfully updated ticket');
            window.location.href = "/";
        },
        error: function () {
            alert('Error: Could not update ticket')
        }
    })
})