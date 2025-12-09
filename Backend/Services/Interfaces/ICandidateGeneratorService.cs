namespace ASP1.Backend.Services.Interfaces;

public interface ICandidateGeneratorService
{
    Task StartGeneratingAsync(CancellationToken cancellationToken);
    void StopGenerating();
}

