using MyBook.DAL.Enums;

namespace MyBook.BLL.DTOModels;

public class SubscriptionDTO
{
    public Guid Id { get; set; }
    public SubscryptionType Name { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public float Cost { get; set; }
}