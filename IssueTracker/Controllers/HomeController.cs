namespace IssueTracker.Controllers;

public class HomeController(ApplicationDbContext context) : Controller
{
    public IActionResult Index()
    {
        var tickets = context.Tickets.AsEnumerable();

        return View(tickets);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Ticket ticket)
    {
        if (!ModelState.IsValid) return View(ticket);

        ticket.Status = Status.Open;
        ticket.CreatedDate = DateTime.Now;

        context.Tickets.Add(ticket);
        await context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpDelete("delete/tickets/{id}")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();

        var ticket = await context.Tickets.FirstOrDefaultAsync(t => t.Id == id);

        if (ticket is null) return NotFound();

        context.Tickets.Remove(ticket);
        await context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("get/ticket/{id}")]
    public async Task<IActionResult> GetTicket(int id)
    {
        var ticket = await context.Tickets.FindAsync(id);

        if (ticket is null) return NotFound();

        return Ok(ticket);
    }


    [HttpPut("edit/ticket")]
    public async Task<IActionResult> PutTicket(Ticket ticket)
    {
        context.Attach(ticket);

        var entry = context.Entry(ticket);

        foreach (var property in entry.Properties)
        {
            if (property.Metadata.Name != nameof(ticket.CreatedDate) &&
                property.Metadata.Name != nameof(ticket.Id)) property.IsModified = true;
        }

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!context.Tickets.Any(e => e.Id == ticket.Id)) return NotFound();
            throw;
        }

        return Ok();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}