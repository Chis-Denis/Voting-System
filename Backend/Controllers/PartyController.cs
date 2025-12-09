using Microsoft.AspNetCore.Mvc;
using ASP1.Backend.Services.Interfaces;
using ASP1.Backend.Domain.Entities;

namespace ASP1.Backend.Controllers;

public class PartyController : Controller
{
    private readonly IPartyService _partyService;

    public PartyController(IPartyService partyService)
    {
        _partyService = partyService;
    }

    public async Task<IActionResult> Index()
    {
        var parties = await _partyService.GetAllPartiesAsync();
        return View(parties);
    }

    public async Task<IActionResult> Details(int id)
    {
        var party = await _partyService.GetPartyByIdAsync(id);
        if (party == null)
            return NotFound();

        return View(party);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Party party)
    {
        if (!ModelState.IsValid)
            return View(party);

        try
        {
            await _partyService.CreatePartyAsync(party);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            return View(party);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var party = await _partyService.GetPartyByIdAsync(id);
        if (party == null)
            return NotFound();

        return View(party);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Party party)
    {
        if (!ModelState.IsValid)
            return View(party);

        try
        {
            await _partyService.UpdatePartyAsync(party);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            return View(party);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var party = await _partyService.GetPartyByIdAsync(id);
        if (party == null)
            return NotFound();

        return View(party);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _partyService.DeletePartyAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
