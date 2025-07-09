namespace IssueTracker.Models;

public class Ticket
{
    public int Id { get; set; }
    [MaxLength(1000)] public string Title { get; set; } = string.Empty;
    [MaxLength(1000)] public string Description { get; set; } = string.Empty;
    public Priority Priority { get; set; }
    public DateTime CreatedDate { get; set; }
    public Status Status { get; set; }
}