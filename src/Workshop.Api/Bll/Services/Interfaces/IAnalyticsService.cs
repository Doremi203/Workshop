using Workshop.Api.Bll.Models;

namespace Workshop.Api.Bll.Services.Interfaces;

public interface IAnalyticsService
{
    void UpdateMaxWeightAndDistanceForHeaviest(GoodModel[] goods, int? distance = null);
    void UpdateMaxVolumeAndDistanceForLargest(GoodModel[] goods, int? distance = null);
    void UpdateOverallPriceAndGoodsCount(double price, int goodsCount);
    AnalyticsModel GetAnalytics();
}