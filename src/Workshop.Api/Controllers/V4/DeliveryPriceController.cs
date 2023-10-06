using Microsoft.AspNetCore.Mvc;
using Workshop.Api.Requests.V4;
using Workshop.Api.Responses.V4;
using Workshop.Bll.Services.Interfaces;

namespace Workshop.Api.Controllers.V4;

[ApiController]
[Route("v4/[controller]")]
public class DeliveryPriceController : ControllerBase
{
    private readonly IPriceCalculatorService _priceCalculatorService;

    public DeliveryPriceController(
        IPriceCalculatorService priceCalculatorService
        )
    {
        _priceCalculatorService = priceCalculatorService;
    }
    
    [HttpPost("calculate")]
    public async Task<CalculateResponse> Calculate(
        CalculateRequest request,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
    
    [HttpPost("get-history")]
    public GetHistoryResponse[] GetHistory(GetHistoryRequest request)
    {
        var log = _priceCalculatorService.QueryLog(request.Take);

        var mappedLog = log
            .Select(model => new GetHistoryResponse(
                new CargoResponse(model.Volume, model.Weight),
                model.Price, model.Distance))
            .ToArray();

        return mappedLog;
    }
}
