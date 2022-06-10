namespace MyBook.Entity;

public class Author
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Image { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<Book> Books { get; set; } = null!;
}