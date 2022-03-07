using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entities;

namespace WebApiAutores.Controllers
{
    [ApiController]
    // Fill the routing with the name of the controller
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {

        private readonly ApplicationDbContext context;

        public AuthorsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Author>>> findAll()
        {
            return await context.Authors.Include(author => author.Books).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Author>> findById(int id)
        {
            Author author = await context.Authors.Include(author => author.Books)
                .FirstOrDefaultAsync(author => author.Id == id);

            if (author == null) return NotFound($"Author with Id {id} not found");

            return author;
        }


        [HttpGet("{name}")]
        public async Task<ActionResult<Author>> findByName([FromRoute] String name)
        {
            Author author = await context.Authors.Include(author => author.Books)
                .FirstOrDefaultAsync(author => author.Name.ToUpper().Contains(name.ToUpper()));

            if (author == null) return NotFound($"Author with name {name} not found");

            return author;
        }

        [HttpGet("nameFromQuery")]
        public async Task<ActionResult<Author>> findByNameFromQuery([FromQuery] String name)
        {
            Author author = await context.Authors.Include(author => author.Books)
                .FirstOrDefaultAsync(author => author.Name.ToUpper().Contains(name.ToUpper()));

            if (author == null) return NotFound($"Author with name {name} not found");

            return author;
        }


        [HttpPost]
        public async Task<ActionResult> create([FromBody] Author _author)
        {

            bool isExist = await context.Authors.AnyAsync(author => author.Name.Equals(_author.Name));

            if (isExist) return BadRequest($"There is already an author with the name {_author.Name}");

            context.Add(_author);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> update(Author author, int id)
        {
            if (author.Id != id) return BadRequest("Author Id does not match URL Id");

            bool exists = await context.Authors.AnyAsync(author => author.Id == id);
            if (!exists) return NotFound();

            context.Update(author);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> delete(int id)
        {
            bool exists = await context.Authors.AnyAsync(author => author.Id == id);
            if (!exists) return NotFound();

            context.Remove(new Author() { Id = id });
            await context.SaveChangesAsync();

            return Ok();
        }

    }
}
