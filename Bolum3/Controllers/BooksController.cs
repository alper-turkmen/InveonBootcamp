using Bolum3.Models;
using Bolum3.Models.Exceptions;
using Bolum3.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bolum3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooksAsync()
        {
            // throw new System.Exception("Unexpected error occurred"); unexpected exception test
            // throw new CustomBookNotFoundException("Book not found"); custom exception test
            var result = await _bookService.GetAllBooksAsync();
            return Ok(result);
        }

        [HttpGet("WithPage")]
        public async Task<IActionResult> GetBooksPagedAsync([FromQuery] int page =1, [FromQuery] int pageSize=10)
        {
            var result = await _bookService.GetBooksPagedAsync(page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var result = _bookService.GetBookById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookAsync([FromBody] Book book)
        {
            var result = await _bookService.CreateBookAsync(book);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookAsync(int id, [FromBody] Book book)
        {
            book.Id = id;
            var result = await _bookService.UpdateBookAsync(book);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookAsync(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            return Ok(result); 
        }
    }
}