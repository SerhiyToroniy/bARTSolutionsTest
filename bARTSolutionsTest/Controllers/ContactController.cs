using bARTSolutionsTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bARTSolutionsTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        Models.AppContext.AppContext db;

        public ContactController(Models.AppContext.AppContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> Get()
        {
            return await db.contacts.ToListAsync();
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<Contact>> Get(string email)
        {
            Contact user = await db.contacts.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        [HttpPost]
        public async Task<ActionResult<Contact>> Post(Contact user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            db.contacts.Add(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult<Contact>> Put(Contact user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!db.contacts.Any(x => x.Email == user.Email))
            {
                return NotFound();
            }

            db.Update(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult<Contact>> Delete(string email)
        {
            Contact user = db.contacts.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return NotFound();
            }
            db.contacts.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
    }
}
