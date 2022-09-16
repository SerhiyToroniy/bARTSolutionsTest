using bARTSolutionsTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bARTSolutionsTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        Models.AppContext.AppContext db;

        public AccountController(Models.AppContext.AppContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> Get()
        {
            return await db.accounts.Include(s => s.Contacts).ToListAsync();
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Account>> Get(string name)
        {
            Account user = await db.accounts.Include(s=>s.Contacts).FirstOrDefaultAsync(x => x.Name == name);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        [HttpPost]
        public async Task<ActionResult<Account>> Post(Account user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            db.accounts.Add(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult<Account>> Put(Account user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!db.accounts.Any(x => x.Name == user.Name))
            {
                return NotFound();
            }

            db.Update(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult<Account>> Delete(string name)
        {
            Account user = db.accounts.FirstOrDefault(x => x.Name == name);
            if (user == null)
            {
                return NotFound();
            }
            db.accounts.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
    }
}
