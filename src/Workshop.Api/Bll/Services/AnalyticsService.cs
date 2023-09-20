using Workshop.Api.Bll.Models;
using Workshop.Api.Bll.Services.Interfaces;
using Workshop.Api.Dal.Repositories.Interfaces;

namespace Workshop.Api.Bll.Services;

public class AnalyticsService : IAnalyticsService
{
    private readonly IStorageRepository _storageRepository;

    public AnalyticsService(IStorageRepository storageRepository)
    {
        _storageRepository = storageRepository;
    }

    public AnalyticsModel GetAnalytics()
    {
        var logs = _storageRepository.Query();
        var maxWeight = logs.Max(model => model.MaxWeight);
        var maxVolume = logs.Max(model => model.MaxVolume);
        var distanceForHeaviest = logs
            .Where(model => model.MaxWeight == maxWeight)
            .Select(model => model.Distance)
            .FirstOrDefault();
        var distanceForLargest = logs
            .Where(model => model.MaxVolume == maxVolume)
            .Select(model => model.Distance)
            .FirstOrDefault();

        var weightAvgPrice = logs.Sum(entity => entity.Price) / logs.Sum(entity => entity.GoodsCount);

        var result = new AnalyticsModel(
            maxVolume,
            maxWeight,
            distanceForLargest,
            distanceForHeaviest,
            weightAvgPrice);
        
        return result;
    }
}