namespace IssueTracker.Controllers;

public class TicketsApiController(ITicketRepository repository) : Controller
{
    public async Task<IActionResult> Index()
    {
        var tickets = await repository.GetAllAsync();

        return View(tickets);
    }

    [HttpPost("create/ticket")]
    public async Task<IActionResult> Create(Ticket ticket)
    {
        if (!ModelState.IsValid) return Error();

        ticket.CreatedDate = DateTime.Now;

        await repository.CreateAsync(ticket);

        return RedirectToAction(nameof(Index));
    }

    [HttpDelete("delete/tickets/{id}")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();

        var ticket = await repository.GetByIdAsync(id.Value);

        if (ticket is null) return NotFound();

        await repository.DeleteAsync(ticket);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("get/ticket/{id}")]
    public async Task<IActionResult> GetTicket(int id)
    {
        var ticket = await repository.GetByIdAsync(id);

        if (ticket is null) return NotFound();

        return Ok(ticket);
    }


    [HttpPut("edit/ticket")]
    public async Task<IActionResult> PutTicket(Ticket ticket)
    {
        await repository.UpdateAsync(ticket);

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