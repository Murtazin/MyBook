using MyBook.BLL.DTOModels;

namespace MyBook.BLL.Interfaces;

public interface IAuthorService
{
    Task UploadAuthor(AuthorDTO authorDto);
    Task<AuthorDTO> GetAuthor(Guid id);
    Task<IEnumerable<AuthorDTO>> GetAuthors();
    Task RemoveAuthor(Guid id);
}