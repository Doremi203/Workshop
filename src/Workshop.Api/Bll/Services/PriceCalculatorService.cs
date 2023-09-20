using Workshop.Api.Bll.Models;
using Workshop.Api.Bll.Services.Interfaces;
using Workshop.Api.Dal.Entities;
using Workshop.Api.Dal.Repositories.Interfaces;

namespace Workshop.Api.Bll.Services;

public class PriceCalculatorService : IPriceCalculatorService
{
    private const double VolumeRatio = 3.27d;
    private const double WeightRatio = 1.34d;
    
    private const double ConversionRatioMmToCm = 0.001d;
    private const int ConversionRatioKgToGr = 1000;
    
    private readonly IStorageRepository _storageRepository;

    public PriceCalculatorService(
        IStorageRepository storageRepository
        )
    {
        _storageRepository = storageRepository;
    }
    
    public double CalculatePrice(GoodModel[] goods, int? distance = null)
    {
        if (!goods.Any())
        {
            throw new ArgumentException("Goods must not be empty.");
        }
        
        var volumePrice = CalculatePriceByVolume(goods, out var volume);

        var weightPrice = CalculatePriceByWeight(goods, out var weight);

        var resultPrice = Math.Max(volumePrice, weightPrice);
        
        var finalPrice = distance.HasValue
            ? resultPrice * distance.Value
            : resultPrice;

        _storageRepository.Save(new StorageEntity(
            volume,
            resultPrice,
            DateTime.UtcNow,
            weight,
            distance));
        
        return finalPrice;
    }

    private static double CalculatePriceByWeight(GoodModel[] goods, out double weightInKg)
    {
        weightInKg = goods
            .Where(good => good.Weight.HasValue)
            .Sum(good => good.Weight!.Value);

        var weightPrice = WeightRatio * weightInKg;
        return weightPrice;
    }

    private static double CalculatePriceByVolume(GoodModel[] goods, out int volume)
    {
        volume = goods
            .Sum(good => good.Length * good.Width * good.Height);

        var volumePrice = VolumeRatio * volume * ConversionRatioMmToCm;
        return volumePrice;
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
                entity.Price,
                entity.Weight,
                entity.Distance))
            .ToArray();

        return mappedLog;
    }

    public void DeleteLogs()
    {
        _storageRepository.DeleteAll();
    }
}