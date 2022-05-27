using MyBook.DAL.Enums;

namespace MyBook.DAL.Entities;

public class Subscription
{
    public Guid Id { get; set; }
    public SubscryptionType Name { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public float Cost { get; set; }
}