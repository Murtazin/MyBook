using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBook.DataAccess;

namespace MyBook.WEB.Controllers;

public class CatalogController : Controller
{
    private readonly ApplicationContext _context;

    public CatalogController(ApplicationContext context) =>
        _context = context;

    [HttpGet]
    public async Task<IActionResult> Books() =>
        View(await _context.Books.Include(a => a.Author).ToListAsync());

    [HttpGet]
    public async Task<IActionResult> FreeBooks() =>
        View(await _context.Books
            .Include(a => a.Author)
            .Where(s => s.SubType == 0)
            .ToListAsync());

    [HttpGet]
    public async Task<IActionResult> TopBooks() =>
        View(await _context.Books
            .Include(a => a.Author)
            .ToListAsync());

    [HttpGet]
    public async Task<IActionResult> Novelties() =>
        View(await _context.Books
            .Include(a => a.Author)
            .ToListAsync());

    [Authorize(Roles = "UserSub, Admin")]
    [HttpGet]
    public async Task<IActionResult> Premium() =>
        View(await _context.Books
            .Include(a => a.Author)
            .Where(s => s.SubType == 1)
            .ToListAsync());

    [HttpGet]
    public async Task<IActionResult> BookDetails(Guid id)
    {
        var book = await _context.Books
            .Include(a => a.Author)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book is null)
            return RedirectToAction("PageNotFound", "Home");

        return View(book);
    }

    [HttpGet]
    public async Task<IActionResult> AuthorDetails(Guid id)
    {
        var author = await _context.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (author is null)
            return RedirectToAction("PageNotFound", "Home");

        return View(author);
    }

    [HttpGet]
    public async Task<IActionResult> Search(int selectId, string? keyword)
    {
        if (keyword == null)
            return RedirectToAction("PageNotFound", "Home");

        if (selectId == 1)
        {
            var allBooks = await _context.Books
                .Include(a => a.Author)
                .ToListAsync();

            var books = allBooks
                .Where(x => string.Concat(x.Title.ToLower().Where(c => !char.IsWhiteSpace(c)))
                    .Contains(string.Concat(keyword.ToLower().Where(c => !char.IsWhiteSpace(c)))))
                .ToList();

            books.AddRange(allBooks
                .Where(x => string.Concat(x.Author.FullName.ToLower().Where(c => !char.IsWhiteSpace(c)))
                    .Contains(string.Concat(keyword.ToLower().Where(c => !char.IsWhiteSpace(c))))));

            return View("SearchBook", books);
        }

        if (selectId == 2)
        {
            var authors = (await _context.Authors.ToListAsync())
                .Where(x => string.Concat(x.FullName.ToLower().Where(c => !char.IsWhiteSpace(c)))
                    .Contains(string.Concat(keyword.ToLower().Where(c => !char.IsWhiteSpace(c)))));

            return View("SearchAuthor", authors);
        }

        return RedirectToAction("PageNotFound", "Home");
    }
}