using Workshop.Bll.Entities;
using Workshop.Bll.Separated;

namespace Workshop.Dal.Dal.Repositories;

public class StorageRepository : IStorageRepository
{
    private readonly List<StorageEntity> _storage = new();
    
    public void Save(StorageEntity entity)
    {
        _storage.Add(entity);
    }

    public StorageEntity[] Query()
    {
        return _storage.ToArray();
    }

    public void DeleteAll()
    {
        _storage.Clear();
    }
}