using BookList.Api.Dtos;
using BookList.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookList.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(IBookService bookService) : ControllerBase
    {
        private readonly IBookService _bookService = bookService;

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if(book == null)
            {
                return NotFound(new { message = "Book not found" });
            }

            return Ok(book);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] BookRequest bookRequest)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = await _bookService.AddBookAsync(bookRequest);
            if(book == null) 
            {
                return BadRequest(new { message = "Failed to create book" });
            }

            return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult?> Update(int id, [FromBody] BookRequest bookRequest)
        {
            if(!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            var updatedBook = await _bookService.UpdateBookAsync(id, bookRequest);

            return Ok(updatedBook);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _bookService.GetBookByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }
            
            await _bookService.DeleteBookAsync(id);

            return NoContent();
        }
    }
}
