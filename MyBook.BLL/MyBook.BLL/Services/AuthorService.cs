using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MyBook.BLL.DTOModels;
using MyBook.BLL.Interfaces;
using MyBook.DAL.Entities;
using MyBook.DAL.Interfaces;

namespace MyBook.BLL.Services;

public class AuthorService : IAuthorService
{
    IUnitOfWork Database { get; set; }

    public AuthorService(IUnitOfWork database)
    {
        Database = database;
    }
    
    public async Task<AuthorDTO> GetAuthor(Guid id)
    {

        if (id == null)
            throw new ValidationException("Автор с таким id не найден");

        var author = await Database.Authors.Get(id);

        if (author == null)
            throw new ValidationException("Автор не найден");

        return new AuthorDTO() 
        { 
            Id = author.Id,
            Age = author.Age,
            FirstName = author.FirstName,
            SecondName = author.SecondName
        };
    }
    
    public async Task<IEnumerable<AuthorDTO>> GetAuthors()
    {
        var authors = await Database.Authors.GetAll();
        var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Author, AuthorDTO>()).CreateMapper();
        return mapper.Map<IEnumerable<Author>, List<AuthorDTO>>(authors);
    }
    
    public async Task UploadAuthor(AuthorDTO authorDto)
    {
        var authors = await Database.Authors.GetAll();

        if(authors.FirstOrDefault(f => f.FirstName == authorDto.FirstName) != null)
        {
            throw new ValidationException("Автор с таким именем уже существует");
        }

        var config = new MapperConfiguration(cfg => cfg.CreateMap<AuthorDTO, Author>());
        var mapper = new Mapper(config);
        var author = mapper.Map<AuthorDTO, Author>(authorDto);

        await Database.Authors.Create(author);
        Database.Save();
    }
    
    public async Task RemoveAuthor(Guid id)
    {
        await Database.Authors.Delete(id);
        Database.Save();
    }
}