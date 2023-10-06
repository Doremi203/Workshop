namespace Workshop.Api.Requests.V4;

public record GoodProperties(
    int Length,
    int Width,
    int Height,
    double? Weight
);