using Workshop.Api.Bll.Models;

namespace Workshop.Api.Bll.Services.Interfaces;

public interface IPriceCalculatorService
{
    public double CalculatePrice(GoodModel[] goods);
    
    public CalculationLogModel[] QueryLog(int takeCount);
}