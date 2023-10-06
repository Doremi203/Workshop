using Workshop.Bll.Entities;

namespace Workshop.Bll.Separated;

public interface IGoodsRepository
{
    void AddOrUpdate(GoodEntity good);
    
    ICollection<GoodEntity> GetAll();
    GoodEntity Get(int id);
}