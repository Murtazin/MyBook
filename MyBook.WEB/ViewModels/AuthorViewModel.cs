namespace MyBook.WEB.ViewModels;

public class AuthorViewModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public int Age { get; set; }
    public List<BookViewModel> Books { get; set; }
    
    public AuthorViewModel()
    {
        Books = new List<BookViewModel>();
    }
}