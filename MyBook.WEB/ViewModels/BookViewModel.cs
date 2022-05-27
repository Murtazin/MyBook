namespace MyBook.WEB.ViewModels;

public class BookViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public AuthorViewModel Author { get; set; }
    public int CountOfPages { get; set; }
}