using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly LibrarydbContext _context;

        public CategoriesController(LibrarydbContext context)
        {
            _context = context;
        }

        [HttpGet("feladat11")]
        public async Task<ActionResult<CategoriesController>> Get()
        {
            var cat = await _context.Categories.Include(book => book.Books).ToListAsync();

            if (cat != null)
            {
                return Ok(cat);
            }

            Exception e = new Exception();
            return BadRequest(e.Message);

        }
    }
}
