using BookList.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await userService.GetUserByIdAsync(id);
            if (user == null) {
                return NotFound(new { message = "User not found"});
            }

            return Ok(user);
        }
    }
}
