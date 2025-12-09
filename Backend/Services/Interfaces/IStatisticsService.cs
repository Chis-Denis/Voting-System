using ASP1.Backend.ViewModels;

namespace ASP1.Backend.Services.Interfaces;

public interface IStatisticsService
{
    Task<StatisticsViewModel> GetStatisticsAsync();
}

