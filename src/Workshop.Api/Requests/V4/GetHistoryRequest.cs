namespace Workshop.Api.Requests.V4;

public record GetHistoryRequest(
    long UserId,
    int Take,
    int Skip
);