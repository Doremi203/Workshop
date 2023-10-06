namespace Workshop.Api.Responses.V4;

public record GetHistoryResponse(
    CargoResponse Cargo,
    double Price,
    double? Distance
    );