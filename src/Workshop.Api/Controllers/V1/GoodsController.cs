using Microsoft.AspNetCore.Mvc;
using Workshop.Api.Bll.Models;
using Workshop.Api.Responses.V1;
using Workshop.Bll.Entities;
using Workshop.Bll.Separated;
using Workshop.Bll.Services.Interfaces;

namespace Workshop.Api.Controllers.V1;

[Route("goods")]
[ApiController]
public class GoodsController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IGoodsRepository _goodsRepository;
    private readonly ILogger<GoodsController> _logger;

    public GoodsController(
        IHttpContextAccessor httpContextAccessor,
        IGoodsRepository goodsRepository,
        ILogger<GoodsController> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _goodsRepository = goodsRepository;
        _logger = logger;
    }
    
    [HttpGet]
    public ICollection<GoodEntity> GetAll()
    {
        return _goodsRepository.GetAll();
    }

    [HttpGet("{id:int}")]
    public CalculateResponse Calculate(
        [FromServices] IPriceCalculatorService _priceCalculatorService,
        int id)
    {
        
        _logger.LogInformation(_httpContextAccessor.HttpContext.Request.Path);
        
        var good = _goodsRepository.Get(id);

        var model = new GoodModel(
            good.Length,
            good.Width,
            good.Height,
            null);

        var result = _priceCalculatorService.CalculatePrice(new[] { model });
        return new CalculateResponse(result);
    }
}