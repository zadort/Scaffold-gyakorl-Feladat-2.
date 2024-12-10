using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly LibrarydbContext _context;

        public AuthorsController(LibrarydbContext context)
        {
            _context = context;
        }

        [HttpGet("feladat9")]
        public async Task<ActionResult<Author>> Get(string name)
        {
            var author = await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(author => author.AuthorName == name);

            if (author != null)
            {
                return Ok(author);
            }

            return NotFound();

        }

        [HttpGet("feladat12")]
        public async Task<ActionResult<string>> GetCountOfAuthors()
        {
            var numOfAuthors = await _context.Authors.CountAsync();

            if (numOfAuthors != null)
            {
                return Ok($"Szerzők száma {numOfAuthors}");
            }

            return BadRequest("Nem lehet csatlakozni az adatbázishoz.");
        }
    }
}
