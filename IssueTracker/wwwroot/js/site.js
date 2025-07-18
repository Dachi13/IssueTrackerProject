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
                $('button[data-ticket-id="' + ticketIdToDelete + '"]').closest('.col-md-4').fadeOut(500, function () {
                    $(this).remove();
                });
                var alertHtml = '<div class="alert alert-info alert-dismissible fade show" role="alert">Ticket has been deleted.<button type="button" class="btn-close" data-bs-dismiss="alert"></button></div>';
                $('#alert-placeholder').html(alertHtml).delay(3000).fadeOut('slow');
            },
            error: function () {
                alert('Error: Could not delete the ticket.');
                $('#deleteConfirmModal').modal('hide');
            }
        });
    });

    $('.edit-ticket-btn').on('click', function () {
        var ticketId = $(this).data('ticket-id');
        $.ajax({
            url: 'get/ticket/' + ticketId,
            method: 'GET',
            success: function (ticket) {
                $('#edit-ticket-id').val(ticket.id);
                $('#edit-title').val(ticket.title);
                $('#edit-description').val(ticket.description);
                $('#edit-priority').val(ticket.priority);
                $('#edit-status').val(ticket.status);
            },
            error: function () {
                alert('Error: Could not load ticket data');
            }
        });
    });

    $('#saveChangesBtn').on('click', function () {
        var ticketData = {
            Id: parseInt($('#edit-ticket-id').val()),
            Title: $('#edit-title').val(),
            Description: $('#edit-description').val(),
            Priority: parseInt($('#edit-priority').val()),
            Status: parseInt($('#edit-status').val()),
            CreatedDate: new Date().toISOString()
        };

        $.ajax({
            url: 'edit/ticket',
            method: 'PUT',
            data: ticketData,
            success: function () {
                var modal = bootstrap.Modal.getInstance(document.getElementById('editTicketModal'));
                modal.hide();

                var row = $('button[data-ticket-id="' + ticketData.Id + '"]').closest('tr');
                row.find('td:eq(0)').text(ticketData.Title);
                row.find('td:eq(1)').text($('#edit-description').val());
                row.find('td:eq(2)').text($('#edit-priority option:selected').text());
                row.find('td:eq(3)').text($('#edit-status option:selected').text());

                var alertHtml = '<div class="alert alert-success alert-dismissible fade show" role="alert">Ticket updated successfully!<button type="button" class="btn-close" data-bs-dismiss="alert"></button></div>';
                $('#alert-placeholder').html(alertHtml).fadeIn().delay(5000).fadeOut('slow');
            },
            error: function () {
                var errorHtml = '<div class="alert alert-danger alert-dismissible fade show" role="alert">Error: Could not save changes.<button type="button" class="btn-close" data-bs-dismiss="alert"></button></div>';
                $('#alert-placeholder').html(errorHtml);
            }
        });
    });

    $('#createTicketBtn').on('click', function () {
        var ticketData = {
            Title: $('#ticket-title').val(),
            Description: $('#ticket-description').val(),
            Priority: $('#ticket-priority').val(),
            Status: $('#ticket-status').val()
        };

        $.ajax({
            url: 'create/ticket',
            method: 'POST',
            data: ticketData,
            success: function () {
                window.location.href = "/";

                var alertHtml = '<div class="alert alert-success alert-dismissible fade show" role="alert">Ticket created successfully!<button type="button" class="btn-close" data-bs-dismiss="alert"></button></div>';
                $('#alert-placeholder').html(alertHtml).fadeIn().delay(5000).fadeOut('slow');

            },
            error: function () {
                var errorHtml = '<div class="alert alert-danger alert-dismissible fade show" role="alert">Error: Could not create ticket.<button type="button" class="btn-close" data-bs-dismiss="alert"></button></div>';
                $('#alert-placeholder').html(errorHtml);
            }
        })
    })

    $('.view-details-btn').on('click', function() {
        var ticketId = $(this).data('ticket-id');

        $('#details-title').text('Loading...');
        $('#details-description').text('');
        $('#details-status').text('');
        $('#details-priority').text('');
        $('#details-created').text('');

        $.ajax({
            url: '/get/ticket/' + ticketId,
            method: 'GET',
            success: function(ticket) {
                var statusText = ["Open", "InProgress", "Closed"][ticket.status];
                var priorityText = ["Low", "Medium", "High"][ticket.priority];
                var createdDate = new Date(ticket.createdDate).toLocaleDateString();

                $('#details-title').text(ticket.title);
                $('#details-description').text(ticket.description);
                $('#details-status').text(statusText);
                $('#details-priority').text(priorityText);
                $('#details-created').text(createdDate);
            },
            error: function() {
                $('#details-title').text('Error');
                $('#details-description').text('Could not load ticket details.');
            }
        });
    }); 
});