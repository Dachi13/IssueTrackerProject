namespace IssueTracker.Models;

public class Ticket
{
    public string Title { get; set; } = string.Empty;
    public Priority Priority { get; set; }
    public Status Status { get; set; }
}