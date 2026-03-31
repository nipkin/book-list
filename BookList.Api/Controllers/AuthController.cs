using BookList.Api.Dtos;
using BookList.Api.Repositories;
using BookList.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService, IUserRepository userRepository) : ControllerBase
    {
        private void SetAuthCookie(string token)
        {
            Response.Cookies.Append("AuthToken", token, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
                Path = "/",
                Expires = DateTime.UtcNow.AddHours(1)
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if(!ModelState.IsValid) {                 
                return BadRequest(ModelState);
            }

            var response = await authService.AuthUserAsync(request);
            if (!response.Success) {                 
                return BadRequest(response.ErrorMessage);
            }

            SetAuthCookie(response.Token);

            return Ok(new { response.Success });
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] UserRequest value)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await authService.AddUserAsync(value);
            if(!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response);
        }

        [Authorize]
        [HttpGet("check")]
        public async Task<IActionResult> Check()
        {
            var username = User.Identity!.Name!;
            var user = await userRepository.GetUserByNameAsync(username);
            if (user == null) return Unauthorized();

            var freshToken = authService.CreateToken(user);
            SetAuthCookie(freshToken);

            return Ok(new { authenticated = true });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Append("AuthToken", "", new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
                Path = "/",
                Expires = DateTime.UtcNow.AddDays(-1)
            });

            return Ok(new { success = true });
        }
    }
}
