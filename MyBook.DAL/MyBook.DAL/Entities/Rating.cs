namespace MyBook.DAL.Entities;

public class Rating
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public float Mark { get; set; }
}