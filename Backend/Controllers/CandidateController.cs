using Microsoft.AspNetCore.Mvc;
using ASP1.Backend.Services.Interfaces;
using ASP1.Backend.ViewModels;

namespace ASP1.Backend.Controllers;

public class CandidateController : Controller
{
    private readonly ICandidateService _candidateService;
    private readonly ICandidateGeneratorService _generatorService;

    public CandidateController(
        ICandidateService candidateService,
        ICandidateGeneratorService generatorService)
    {
        _candidateService = candidateService;
        _generatorService = generatorService;
    }

    public async Task<IActionResult> Index()
    {
        var candidates = await _candidateService.GetAllCandidatesAsync();
        return View(candidates);
    }

    public async Task<IActionResult> Details(int id)
    {
        var candidate = await _candidateService.GetCandidateByIdAsync(id);
        if (candidate == null)
            return NotFound();

        ViewBag.PartyName = candidate.PartyName;
        return View(candidate);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CandidateViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            await _candidateService.CreateCandidateAsync(model);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var candidate = await _candidateService.GetCandidateByIdAsync(id);
        if (candidate == null)
            return NotFound();

        ViewData["CandidateId"] = candidate.CandidateId;
        return View(candidate);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CandidateViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            await _candidateService.UpdateCandidateAsync(id, model);
            return RedirectToAction(nameof(Index));
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var candidate = await _candidateService.GetCandidateByIdAsync(id);
        if (candidate == null)
            return NotFound();

        return View(candidate);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _candidateService.DeleteCandidateAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StartGenerating()
    {
        await _generatorService.StartGeneratingAsync(CancellationToken.None);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult StopGenerating()
    {
        _generatorService.StopGenerating();
        return RedirectToAction(nameof(Index));
    }
}
