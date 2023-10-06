using Workshop.Bll.Entities;
using Workshop.Bll.Services.Interfaces;

namespace Workshop.Bll.Services;

public class GoodsService : IGoodsService
{
    public record GoodModel(
        string Name,
        int Id,
        int Height,
        int Length,
        int Width,
        int Weight,
        decimal Price
        );
    
    private readonly List<GoodModel> _goods = new()
    {
        new GoodModel("Парик для питомца", 1, 1000, 2000, 3000, 4000, 100),
        new GoodModel("Накидка на телевизор", 2, 1000, 2000, 3000, 4000, 120),
        new GoodModel("Ковер настенный", 3, 2000, 3000, 3000, 5000, 140),
        new GoodModel("Здоровенный ЯЗЪ", 4, 1000, 1000, 4000, 4000, 160),
        new GoodModel("Билет МММ", 5, 3000, 2000, 1000, 5000, 180),
    };
    
    public IEnumerable<GoodEntity> GetGoods()
    {
        var rnd = new Random();
        foreach (var model in _goods)
        {
            var count = rnd.Next(0, 10);

            yield return new GoodEntity(
                model.Id,
                model.Name,
                model.Length,
                model.Width,
                model.Height,
                model.Weight,
                count,
                model.Price);
        }
    }
}