using Microsoft.AspNetCore.Mvc;
using ASP1.Backend.Services.Interfaces;
using ASP1.Backend.ViewModels;
using ASP1.Backend.Domain.Entities;
using ASP1.Backend.Data.Repositories;

namespace ASP1.Backend.Controllers;

public class ElectionController : Controller
{
    private readonly IVoteService _voteService;
    private readonly IElectionService _electionService;
    private readonly ICountyRepository _countyRepository;
    private readonly IElectionCandidateRepository _electionCandidateRepository;

    public ElectionController(
        IVoteService voteService,
        IElectionService electionService,
        ICountyRepository countyRepository,
        IElectionCandidateRepository electionCandidateRepository)
    {
        _voteService = voteService;
        _electionService = electionService;
        _countyRepository = countyRepository;
        _electionCandidateRepository = electionCandidateRepository;
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var electionDetails = await _voteService.GetElectionDetailsAsync(id);
            var counties = await _countyRepository.GetAllCountiesAsync();
            var electionCandidates = await _electionCandidateRepository.GetCandidatesByElectionAsync(id);

            ViewBag.Counties = counties;
            ViewBag.ElectionCandidates = electionCandidates.Select(ec => ec.Candidate).ToList();
            return View(electionDetails);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Vote(VoteViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Details", new { id = model.ElectionId });
        }

        var success = await _voteService.SubmitVoteAsync(model);
        if (success)
        {
            TempData["SuccessMessage"] = "Your vote has been recorded successfully!";
        }
        else
        {
            TempData["ErrorMessage"] = "Unable to submit vote. Please check that the election is active and the candidate is valid.";
        }

        return RedirectToAction("Details", new { id = model.ElectionId });
    }
}

