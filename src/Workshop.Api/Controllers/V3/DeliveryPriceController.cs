using Microsoft.AspNetCore.Mvc;
using Workshop.Api.Bll.Models;
using Workshop.Api.Bll.Services.Interfaces;
using Workshop.Api.Requests.V3;
using Workshop.Api.Responses.V3;

namespace Workshop.Api.Controllers.V3;

[ApiController]
[Route("v3/[controller]")]
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
                .ToArray(), request.Distance);
        return new CalculateResponse(result);
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
