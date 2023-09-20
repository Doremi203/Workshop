using Workshop.Api.Bll.Models;
using Workshop.Api.Bll.Services.Interfaces;
using Workshop.Api.Dal.Entities;
using Workshop.Api.Dal.Repositories.Interfaces;

namespace Workshop.Api.Bll.Services;

public class PriceCalculatorService : IPriceCalculatorService
{
    private readonly IStorageRepository _storageRepository;
    private const double VolumeRatio = 3.27d;
    private const int ConversionRatioMmToCm = 1000;

    public PriceCalculatorService(
        IStorageRepository storageRepository
        )
    {
        _storageRepository = storageRepository;
    }
    
    public double CalculatePrice(GoodModel[] goods)
    {
        if (!goods.Any())
        {
            throw new ArgumentException("Goods must not be empty.");
        }
        
        var volume = goods
            .Sum(good => good.Length * good.Width * good.Height);
        
        var price = VolumeRatio * volume / ConversionRatioMmToCm;

        _storageRepository.Save(new StorageEntity(
            volume,
            price,
            DateTime.UtcNow));
        
        return price;
    }

    public CalculationLogModel[] QueryLog(int takeCount)
    {
        if (takeCount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(takeCount), takeCount, "Take count must be greater than zero.");
        }
        
        var log = _storageRepository.Query()
            .OrderByDescending(entity => entity.At)
            .Take(takeCount)
            .ToArray();
        
        var mappedLog = log
            .Select(entity => new CalculationLogModel(
                entity.Volume,
                entity.Price))
            .ToArray();

        return mappedLog;
    }
}