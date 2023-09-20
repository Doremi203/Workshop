using Workshop.Api.Bll.Models;

namespace Workshop.Api.Bll.Services.Interfaces;

public interface IPriceCalculator
{
    public double CalculatePrice(GoodModel[] goods);
    
    public CalculationLogModel[] QueryLog(int take);
}