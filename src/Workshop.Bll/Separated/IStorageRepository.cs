using Workshop.Bll.Entities;

namespace Workshop.Bll.Separated;

public interface IStorageRepository
{
    void Save(StorageEntity entity);
    StorageEntity[] Query();
    void DeleteAll();
}