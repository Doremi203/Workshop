using Microsoft.AspNetCore.Mvc;
using Workshop.Api.Bll.Models;
using Workshop.Api.Requests.V2;
using Workshop.Api.Responses.V2;
using Workshop.Bll.Services.Interfaces;

namespace Workshop.Api.Controllers.V2;

[ApiController]
[Route("v2/[controller]")]
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
    public CalculateResponse Calculate(CalculateRequest request)
    {
        var result = _priceCalculatorService.CalculatePrice(
            request.Goods
                .Select(good => new GoodModel(
                    good.Length,
                    good.Width,
                    good.Height,
                    good.Weight))
                .ToArray());
        return new CalculateResponse(result);
    }
    
    [HttpPost("get-history")]
    public GetHistoryResponse[] GetHistory(GetHistoryRequest request)
    {
        var log = _priceCalculatorService.QueryLog(request.Take);

        var mappedLog = log
            .Select(model => new GetHistoryResponse(
                new CargoResponse(model.Volume, model.Weight),
                model.Price))
            .ToArray();

        return mappedLog;
    }
}
