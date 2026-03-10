using BookList.Api.Dtos;
using BookList.Api.Helpers;
using BookList.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookList.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController(IQuoteService quoteService) : ControllerBase
    {
        private readonly IQuoteService _quoteService = quoteService;

        [HttpGet("user")]
        public async Task<IActionResult> UserQuotes()
        {
            if (!User.TryGetUserId(out var userId))
            {
                return BadRequest(new { message = "User not found" });
            }

            var quotes = await _quoteService.GetUserQuotesAsync(userId);
            if (quotes == null)
            {
                return BadRequest(new { message = "User not found" });
            }

            return Ok(quotes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var quote = await _quoteService.GetQuoteByIdAsync(id);
            if(quote == null)
            {
                return NotFound(new { message = "Quote not found" });
            }

            return Ok(quote);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] QuoteRequest quoteRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!User.TryGetUserId(out var userId))
            {
                return BadRequest(new { message = "User not found" });
            }

            var quotes = await _quoteService.GetUserQuotesAsync(userId);
            if(quotes != null && quotes.UserQuotes.Count >= 5) {
                return BadRequest(new { message = "You have reached the maximum number of quotes (5)" });
            }

            var quote = await _quoteService.AddQuoteAsync(userId, quoteRequest);
            if(quote == null) {
                return BadRequest(new { message = "Failed to create quote" });
            }

            return CreatedAtAction(nameof(Get), new { id = quote.Id }, quote);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult?> Update(int id, [FromBody] QuoteRequest quoteRequest)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            var updatedBook = await _quoteService.UpdateQuoteAsync(id, quoteRequest);

            return Ok(updatedBook);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var quote = await _quoteService.GetQuoteByIdAsync(id);
            if(quote == null) {
                return NotFound(new { message = "Quote not found" });
            }

            await _quoteService.DeleteQuoteByIdAsync(id);

            return NoContent();
        }
    }
}
