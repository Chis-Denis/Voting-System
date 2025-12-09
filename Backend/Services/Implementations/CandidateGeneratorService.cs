using ASP1.Backend.Data.Repositories;
using ASP1.Backend.Domain.Entities;
using ASP1.Backend.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ASP1.Backend.Services.Implementations;

public class CandidateGeneratorService : ICandidateGeneratorService
{
    private readonly IServiceProvider _serviceProvider;
    private CancellationTokenSource? _cancellationTokenSource;
    private Task? _generatorTask;

    public CandidateGeneratorService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartGeneratingAsync(CancellationToken cancellationToken)
    {
        if (_generatorTask != null && !_generatorTask.IsCompleted)
            return Task.CompletedTask;

        _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        _generatorTask = Task.Run(async () =>
        {
            using var scope = _serviceProvider.CreateScope();
            var candidateRepository = scope.ServiceProvider.GetRequiredService<ICandidateRepository>();
            var partyRepository = scope.ServiceProvider.GetRequiredService<IPartyRepository>();

            var rnd = new Random();
            string[] names = { "Alex", "Maria", "Ion", "Elena", "Vlad", "Ana", "George", "Diana" };
            string[] positions = { "Deputat", "Senator", "Primar", "Consilier" };
            string[] parties = { "PSD", "PNL", "USR", "AUR", "PMP" };

            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                try
                {
                    var name = names[rnd.Next(names.Length)] + " " + names[rnd.Next(names.Length)];
                    var desc = "Generated candidate";
                    var img = "https://randomuser.me/api/portraits/lego/" + rnd.Next(1, 10) + ".jpg";
                    var age = rnd.Next(18, 80);
                    var pos = positions[rnd.Next(positions.Length)];
                    var partyName = parties[rnd.Next(parties.Length)];

                    var party = await partyRepository.GetPartyByNameAsync(partyName);
                    if (party == null)
                    {
                        party = new Party
                        {
                            Name = partyName,
                            Description = "Generated party",
                            LogoUrl = string.Empty
                        };
                        await partyRepository.AddAsync(party);
                    }

                    var candidate = new Candidate
                    {
                        Name = name,
                        Description = desc,
                        ImageUrl = img,
                        Age = age,
                        Position = pos,
                        PartyId = party.PartyId
                    };

                    await candidateRepository.AddAsync(candidate);
                    await Task.Delay(1000, _cancellationTokenSource.Token);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }, _cancellationTokenSource.Token);
        
        return Task.CompletedTask;
    }

    public void StopGenerating()
    {
        _cancellationTokenSource?.Cancel();
    }
}

