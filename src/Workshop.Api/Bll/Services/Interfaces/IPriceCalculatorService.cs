using Workshop.Api.Bll.Models;

namespace Workshop.Api.Bll.Services.Interfaces;

public interface IPriceCalculatorService
{
    public double CalculatePrice(GoodModel[] goods, int? distance = null);
    
    public CalculationLogModel[] QueryLog(int takeCount);
}