using BookList.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) {
                return NotFound(new { message = "User not found"});
            }

            return Ok(user);
        }
    }
}
