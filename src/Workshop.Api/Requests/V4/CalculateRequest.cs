namespace Workshop.Api.Requests.V4;

public record CalculateRequest(
    long UserId,
    GoodProperties[] Goods
);