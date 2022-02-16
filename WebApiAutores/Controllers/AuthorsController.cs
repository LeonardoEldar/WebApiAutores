using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entities;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {

        private readonly ApplicationDbContext context;

        public AuthorsController(ApplicationDbContext context) {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Author>>> findAll() {
            return await context.Authors.Include(author => author.Books).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> create(Author author) {
            context.Add(author);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> update(Author author, int id) {
            if (author.Id != id) return BadRequest("Author Id does not match URL Id");
            
            bool exists = await context.Authors.AnyAsync(author => author.Id == id);
            if (!exists) return NotFound();

            context.Update(author);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> delete(int id) {
            bool exists = await context.Authors.AnyAsync(author => author.Id == id);
            if (!exists) return NotFound();

            context.Remove(new Author() { Id = id });
            await context.SaveChangesAsync();

            return Ok();
        }

    }
}
