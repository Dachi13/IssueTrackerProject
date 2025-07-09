namespace IssueTracker.Controllers;

public class HomeController(ILogger<HomeController> logger, ApplicationDbContext context) : Controller
{
    public IActionResult Index()
    {
        var tickets = new List<Ticket>()
        {
            new Ticket()
            {
                Id = 1,
                Priority = Priority.High,
                Title = "Bug with project",
                Status = Status.Open,
                CreatedDate = DateTime.Now
            },
            new Ticket()
            {
                Id = 2,
                Priority = Priority.High,
                Title = "Naming",
                Status = Status.Open,
                CreatedDate = DateTime.Now
            }
        };
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

        // save to db
        context.Tickets.Add(ticket);
        await context.SaveChangesAsync();

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