using MyBook.DAL.Entities;

namespace MyBook.BLL.DTOModels;

public class BookDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Author Author { get; set; }
    public string Description { get; set; }
    public int CountOfPages { get; set; }
    public DateTime YearOfIssue { get; set; }
}