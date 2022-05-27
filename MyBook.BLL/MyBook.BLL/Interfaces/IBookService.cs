using MyBook.BLL.DTOModels;

namespace MyBook.BLL.Interfaces;

public interface IBookService
{
    Task UploadBook(BookDTO bookDto);
    Task<BookDTO> GetBook(Guid id);
    Task<IEnumerable<BookDTO>> GetBooks();
    Task RemoveBook(Guid id);
}