using Microsoft.AspNetCore.Mvc;
using ASP1.Backend.Services.Interfaces;
using ASP1.Backend.Domain.Entities;

namespace ASP1.Backend.Controllers;

public class HomeController : Controller
{
    private readonly IElectionService _electionService;

    public HomeController(IElectionService electionService)
    {
        _electionService = electionService;
    }

    public async Task<IActionResult> Index(ElectionType? type = null)
    {
        var elections = await _electionService.GetElectionsByTypeAsync(type);
        ViewBag.SelectedType = type;
        return View(elections);
    }

    public IActionResult Privacy()
    {
        return View();
    }
} 