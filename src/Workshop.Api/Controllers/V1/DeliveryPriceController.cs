using Microsoft.AspNetCore.Mvc;
using Workshop.Api.Bll.Models;
using Workshop.Api.Bll.Services.Interfaces;
using Workshop.Api.Requests.V1;
using Workshop.Api.Responses.V1;

namespace Workshop.Api.Controllers.V1;

[ApiController]
[Route("v1/[controller]")]
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
                    null))
                .ToArray());
        return new CalculateResponse(result);
    }
    
    [HttpPost("get-history")]
    public GetHistoryResponse[] GetHistory(GetHistoryRequest request)
    {
        var log = _priceCalculatorService.QueryLog(request.Take);

        var mappedLog = log
            .Select(model => new GetHistoryResponse(
                new CargoResponse(model.Volume),
                model.Price))
            .ToArray();

        return mappedLog;
    }
    
    [HttpPost("delete-history")]
    public DeleteHistoryResponse DeleteHistory(DeleteHistoryRequest request)
    {
        _priceCalculatorService.DeleteLogs();
        return new DeleteHistoryResponse();
    }
}
