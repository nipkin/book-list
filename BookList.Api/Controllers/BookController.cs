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
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await bookService.GetBookByIdAsync(id);
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

            var book = await bookService.AddBookAsync(bookRequest);
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
            var updatedBook = await bookService.UpdateBookAsync(id, bookRequest);

            return Ok(updatedBook);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await bookService.GetBookByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }
            
            var deleted = await bookService.DeleteBookAsync(id);

            if(!deleted) {
                return BadRequest(new { message = "Failed to delete book" });
            }

            return NoContent();
        }
    }
}
