using BookstoreApi.Data;
using BookstoreApi.Dtos.Books;
using BookstoreApi.Dtos.Request;
using BookstoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly AppDbContext _context;

    public BookController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateEditBookRequest request)
    {
        var book = new Book
        {
            Title = request.Title,
            Author = request.Author,
            Gender = request.Gender,
            Price = request.Price,
            Stock = request.Stock,
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return Created();
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Edit([FromBody] CreateEditBookRequest request, [FromRoute] int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        book.Title = request.Title;
        book.Author = request.Author;
        book.Gender = request.Gender;
        book.Price = request.Price;
        book.Stock = request.Stock;
        
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<BookResponse>), StatusCodes.Status201Created)]
    public async Task<IActionResult> GetAll()
    {
        var books = await _context.Books
            .Select(a => new BookResponse
            {
                Id = a.Id,
                Author = a.Author,
                Gender = a.Gender,
                Price = a.Price,
                Stock = a.Stock,
                Title = a.Title
            })
            .ToListAsync();

        return Ok(books);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        return Ok();
    }
}
