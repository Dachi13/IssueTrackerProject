namespace IssueTracker.Controllers;

public class HomeController(ILogger<HomeController> logger, ApplicationDbContext context) : Controller
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

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();

        var ticket = await context.Tickets.FindAsync(id);

        if (ticket is null) return NotFound();

        return View(ticket);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Ticket ticket)
    {
        if (id != ticket.Id) return NotFound();

        if (!ModelState.IsValid) return View(ticket);

        try
        {
            context.Update(ticket);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // This is an advanced case, but good to know:
            // It handles when someone else edits the same record at the same time.
            // We just re-throw for now.
            throw;
        }

        return RedirectToAction(nameof(Index));
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