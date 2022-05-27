namespace MyBook.WEB.ViewModels;

public class SubscriptionViewModel
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public float Cost { get; set; }
}