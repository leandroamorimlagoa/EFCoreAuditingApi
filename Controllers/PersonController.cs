using AuditingApi.Contexts;
using AuditingApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuditingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly BusinessContext context;

        public PersonController(BusinessContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(context.Person);
        }

        [HttpPost]
        public IActionResult Post(Person person)
        {
            var personEntity = context.Person.FirstOrDefault(p => p.Id == person.Id);
            if(personEntity == null)
            {
                personEntity = new Person();
            }
            personEntity.Name = person.Name;
            personEntity.Email = person.Email;

            if(personEntity.Id == 0)
            {
                context.Person.Add(personEntity);
            }
            context.SaveChanges();

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
