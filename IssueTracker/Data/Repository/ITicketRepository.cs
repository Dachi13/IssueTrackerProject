namespace IssueTracker.Data.Repository;

public interface ITicketRepository
{
    Task<Ticket?> GetByIdAsync(int id);
    Task<List<Ticket>> GetAllAsync();
    Task<Ticket> CreateAsync(Ticket ticket);
    Task UpdateAsync(Ticket ticket);
    void Delete(Ticket ticket);
}