using Workshop.Api.Bll.Models;
using Workshop.Api.Bll.Services.Interfaces;
using Workshop.Api.Dal.Repositories.Interfaces;

namespace Workshop.Api.Bll.Services;

public class AnalyticsService : IAnalyticsService
{
    private class AnalyticsData
    {
        public double OverallPrice { get; set; }
        public int OverallGoods { get; set; }
        public double? MaxWeight { get; set; }
        public double? MaxVolume { get; set; }
        public int? DistanceForHeaviest { get; set; }
        public int? DistanceForLargest { get; set; }
    }
    
    private readonly IStorageRepository _storageRepository;

    private readonly AnalyticsData _analyticsData = new();

    public AnalyticsService(IStorageRepository storageRepository)
    {
        _storageRepository = storageRepository;
    }

    public void UpdateMaxWeightAndDistanceForHeaviest(GoodModel[] goods, int? distance = null)
    {
        var maxWeight = goods.Max(model => model.Weight);
        _analyticsData.MaxWeight ??= maxWeight;
        if (maxWeight < _analyticsData.MaxWeight) 
            return;
        _analyticsData.MaxWeight = maxWeight;
        _analyticsData.DistanceForHeaviest = distance;
    }
    
    public void UpdateMaxVolumeAndDistanceForLargest(GoodModel[] goods, int? distance = null)
    {
        var maxVolume = goods.Max(model => model.Height * model.Length * model.Width);
        _analyticsData.MaxVolume ??= maxVolume;
        if (maxVolume < _analyticsData.MaxVolume) 
            return;
        _analyticsData.MaxVolume = maxVolume;
        _analyticsData.DistanceForLargest = distance;
    }

    public void UpdateOverallPriceAndGoodsCount(double price, int goodsCount)
    {
        _analyticsData.OverallPrice += price;
        _analyticsData.OverallGoods += goodsCount;
    }

    public AnalyticsModel GetAnalytics()
    {
        var result = new AnalyticsModel(_analyticsData.MaxVolume, _analyticsData.MaxWeight,
            _analyticsData.DistanceForLargest, _analyticsData.DistanceForHeaviest,
            _analyticsData.OverallPrice / _analyticsData.OverallGoods);
        return result;
    }
}