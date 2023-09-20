using Workshop.Api.Bll.Models;

namespace Workshop.Api.Bll.Services.Interfaces;

public interface IPriceCalculatorService
{
    double CalculatePrice(GoodModel[] goods, int? distance = null);
    
    CalculationLogModel[] QueryLog(int takeCount);
    void DeleteLogs();
}