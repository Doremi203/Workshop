namespace Workshop.Api.Bll.Models;

public record AnalyticsModel(
    double? GoodMostVolume,
    double? GoodMostWeight,
    int? DistanceForGoodWithMostVolume,
    int? DistanceForGoodWithMostWeight,
    double? AveragePriceByGoodsCount
    );