namespace IssueTracker.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Ticket> Tickets { get; set; }
}