using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using bARTSolutionsTest.Models;
using System.Threading.Tasks;

namespace WebAPIApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncidentController : ControllerBase
    {
        bARTSolutionsTest.Models.AppContext.AppContext db;
        public IncidentController(bARTSolutionsTest.Models.AppContext.AppContext context)
        {
            db = context;
            //if (!db.incidents.Any())
            //{
            //    db.incidents.AddRange(CreateIncidents());
            //    db.incidents.Include(s => s.Accounts)
            //    .ThenInclude(t => t.Contacts)
            //    .FirstOrDefault();
            //    db.SaveChanges();
            //}
        }

        private ICollection<Incident> CreateIncidents()
        {
            return new List<Incident>()
            {
                new Incident
                {
                    Name = "IN1",
                    Accounts = new List<Account>() {
                        new Account { Name = "Account1",
                            Contacts = new List<Contact>(){
                                new Contact{ FirstName="FN1", LastName="LN1", Email="email1"},
                                new Contact{ FirstName="FN2", LastName="LN2", Email="email2"}
                            }
                        }
                    }
                },
                new Incident
                {
                    Name = "IN2",
                    Accounts = new List<Account>() {
                        new Account { Name = "Account2",
                            Contacts = new List<Contact>(){
                                new Contact{ FirstName="FN3", LastName="LN3", Email="email3"},
                                new Contact{ FirstName="FN4", LastName="LN4", Email="email4"}
                            }
                        }
                    }
                },
            };
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Incident>>> Get()
        {
            return await db.incidents.Include(s => s.Accounts).ThenInclude(f => f.Contacts).ToListAsync();
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Incident>> Get(string name)
        {
            Incident user = await db.incidents.FirstOrDefaultAsync(x => x.Name == name);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        [HttpPost]
        public async Task<ActionResult<Incident>> Post(Incident user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            db.incidents.Add(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult<Incident>> Put(Incident user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!db.incidents.Any(x => x.Name == user.Name))
            {
                return NotFound();
            }

            db.Update(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult<Incident>> Delete(string name)
        {
            Incident user = db.incidents.FirstOrDefault(x => x.Name == name);
            if (user == null)
            {
                return NotFound();
            }
            db.incidents.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
    }
}