using Workshop.Bll.Entities;
using Workshop.Bll.Separated;

namespace Workshop.Dal.Dal.Repositories;

public class GoodsRepository : IGoodsRepository
{
    private Dictionary<int, GoodEntity> _store = new();
    
    public void AddOrUpdate(GoodEntity good)
    {
        _store.Remove(good.Id);
        _store.Add(good.Id, good);
    }

    public ICollection<GoodEntity> GetAll()
    {
        return _store.Select(pair => pair.Value).ToArray();
    }

    public GoodEntity Get(int id)
    {
        return _store[id];
    }
}