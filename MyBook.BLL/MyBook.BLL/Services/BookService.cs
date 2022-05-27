using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MyBook.BLL.DTOModels;
using MyBook.BLL.Interfaces;
using MyBook.DAL.Entities;
using MyBook.DAL.Interfaces;

namespace MyBook.BLL.Services;

public class BookService : IBookService
{
    IUnitOfWork Database { get; set; }

    public BookService(IUnitOfWork database)
    {
        Database = database;
    }
    
    public async Task<BookDTO> GetBook(Guid id)
    {

        if (id == null)
            throw new ValidationException("Книга с таким id не найдена");

        var book = await Database.Books.Get(id);

        if (book == null)
            throw new ValidationException("Книга не найдена");

        return new BookDTO() 
        { 
            Id = book.Id,
            Title = book.Title,
            // Author = book.Author,
            Description = book.Description,
            CountOfPages = book.CountOfPages,
            YearOfIssue = book.YearOfIssue
        };
    }
    
    public async Task<IEnumerable<BookDTO>> GetBooks()
    {
        var books = await Database.Books.GetAll();
        var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>()).CreateMapper();
        return mapper.Map<IEnumerable<Book>, List<BookDTO>>(books);
    }
    
    public async Task UploadBook(BookDTO bookDto)
    {
        var books = await Database.Books.GetAll();

        if(books.FirstOrDefault(f => f.Title == bookDto.Title) != null)
        {
            throw new ValidationException("Фильм с таким названием уже существует");
        }

        var config = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, Book>());
        var mapper = new Mapper(config);
        var book = mapper.Map<BookDTO, Book>(bookDto);

        await Database.Books.Create(book);
        Database.Save();
    }
    
    public async Task RemoveBook(Guid id)
    {
        await Database.Books.Delete(id);
        Database.Save();
    }
}