using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entities;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public BooksController (ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Book>> find(int id)
        {
            return await context.Books.Include(book => book.Author)
                .FirstOrDefaultAsync(book => book.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> create(Book book)
        {
            Author author = await context.Authors.FirstOrDefaultAsync(author => author.Id == book.AuthorId);

            if (author == null) return BadRequest($"There is no author with Id: {book.AuthorId}");
            
            context.Add(book);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
