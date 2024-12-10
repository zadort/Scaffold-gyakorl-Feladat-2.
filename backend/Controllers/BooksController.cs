using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace backend.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibrarydbContext _context;

        public BooksController(LibrarydbContext context)
        {
            _context = context;
        }

        public object bookId { get; private set; }
        public object title { get; private set; }
        public object publishDate { get; private set; }
        public object authorId { get; private set; }
        public object categoryId { get; private set; }

        [HttpGet("feladat10")]
        public async Task<ActionResult<Book>> Get()
        {
            var books = await _context.Books.ToListAsync();

            if (books != null)
            {
                return Ok(books);
            }

            return BadRequest();
        }

        [HttpPost("feladat13")]
        public async Task<ActionResult<string>> Post(string uid, Book book)
        {
            var builder = WebApplication.CreateBuilder();
            string id = builder.Configuration.GetValue<string>("Code");

            if (uid == id)
            {
                var bok = new Book
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    PublishDate = book.PublishDate,
                    AuthorId = book.AuthorId,
                    CategoryId = book.CategoryId
                };

                if (bok != null)
                {
                    await _context.Books.AddAsync(bok);
                    await _context.SaveChangesAsync();
                    return StatusCode(201, "Könyv hozzáadása sikeresen megtörtént.");
                }

                Exception e = new Exception();
                return BadRequest(e.Message);
            }

            return StatusCode(401, "Nincs jogosultsága új könyv felviteléhez!");
        }
    }
}
