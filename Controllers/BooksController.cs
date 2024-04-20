using AuditingApi.Contexts;
using AuditingApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuditingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BusinessContext context;

        public BooksController(BusinessContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(context.Book);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            var bookEntity = context.Book.FirstOrDefault(f=>f.Id == book.Id);
            if (bookEntity == null)
            {
                bookEntity = new Book();
            }
            bookEntity.Title = book.Title;
            bookEntity.Year = book.Year;

            if(bookEntity.Id == 0)
            {
                context.Book.Add(bookEntity);
            }
            context.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
