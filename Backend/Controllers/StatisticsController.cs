using Microsoft.AspNetCore.Mvc;
using ASP1.Backend.Services.Interfaces;

namespace ASP1.Backend.Controllers;

public class StatisticsController : Controller
{
    private readonly IStatisticsService _statisticsService;

    public StatisticsController(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    public async Task<IActionResult> Index()
    {
        var stats = await _statisticsService.GetStatisticsAsync();
        return View(stats);
    }
}
