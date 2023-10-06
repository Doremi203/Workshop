namespace Workshop.Bll.Entities;

public record StorageEntity(
    double Volume,
    double Price,
    DateTime At,
    double? Weight,
    int? Distance,
    int GoodsCount,
    double? MaxWeight,
    double MaxVolume
    );