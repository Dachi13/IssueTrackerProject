namespace IssueTracker.Data.Repository;

public class TicketRepository(ApplicationDbContext context) : ITicketRepository
{
    public async Task<Ticket?> GetByIdAsync(int id)
    {
        return await context.Tickets.FindAsync(id);
    }

    public async Task<List<Ticket>> GetAllAsync()
    {
        return await context.Tickets.ToListAsync();
    }

    public async Task<Ticket> CreateAsync(Ticket ticket)
    {
        await context.Tickets.AddAsync(ticket);
        await context.SaveChangesAsync();

        return ticket;
    }

    public async Task UpdateAsync(Ticket ticket)
    {
        context.Attach(ticket);

        var entry = context.Entry(ticket);

        foreach (var property in entry.Properties)
        {
            if (property.Metadata.Name != nameof(ticket.CreatedDate) &&
                property.Metadata.Name != nameof(ticket.Id)) property.IsModified = true;
        }

        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Ticket ticket)
    {
        context.Tickets.Remove(ticket);
        await context.SaveChangesAsync();
    }
}