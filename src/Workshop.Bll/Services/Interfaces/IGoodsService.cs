using Workshop.Bll.Entities;

namespace Workshop.Bll.Services.Interfaces;

public interface IGoodsService
{
    IEnumerable<GoodEntity> GetGoods();
}