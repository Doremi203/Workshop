namespace Workshop.Bll.Entities;

public record GoodEntity(
    int Id,
    string Name,
    int Length,
    int Width,
    int Height,
    double Weight,
    int Count,
    decimal Price
    );