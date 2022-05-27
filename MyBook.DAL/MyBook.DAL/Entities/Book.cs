namespace MyBook.DAL.Entities;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    // public Author Author { get; set; }
    public string Description { get; set; }
    public int CountOfPages { get; set; }
    public DateTime YearOfIssue { get; set; }
}