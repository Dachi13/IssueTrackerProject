namespace IssueTracker.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
    public IActionResult Index()
    {
        var tickets = new List<Ticket>()
        {
            new Ticket()
            {
                Priority = Priority.High,
                Title = "Bug with project",
                Status = Status.Open
            },
            new Ticket()
            {
                Priority = Priority.High,
                Title = "Naming",
                Status = Status.Open
            }
        };
        return View(tickets);
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