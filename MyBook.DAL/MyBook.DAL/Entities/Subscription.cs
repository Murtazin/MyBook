namespace MyBook.DAL.Entities;

public class Subscription
{
    public int Id { get; set; }
    public int Price { get; set; }
    public string Name { get; set; } = null!;
    public int Duration { get; set; } //minutes ??
    public string Description { get; set; } = null!;
}