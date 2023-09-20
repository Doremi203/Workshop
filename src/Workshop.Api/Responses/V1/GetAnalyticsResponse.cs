namespace Workshop.Api.Responses.V1;

public record GetAnalyticsResponse(
    double? max_weight,
    double? max_volume,
    int? max_distance_for_heaviest_good,
    int? max_distance_for_largest_good,
    double? wavg_price
    );