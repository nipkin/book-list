using BookList.Api.Domain;
using BookList.Api.Dtos;
using BookList.Api.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookList.Api.Services
{
    public class AuthService(IUserRepository userRepository, IConfiguration configuration) : IAuthService
    {
        private readonly IConfiguration _config = configuration;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly PasswordHasher<AppUser> _hasher = new();

        public async Task<LoginResponse> AuthUserAsync(LoginRequest request)
        {
            var user = await _userRepository.GetUserByNameAsync(request.Username);
            if (user == null) { 
                return new LoginResponse { Success = false, ErrorMessage = "User does not exist" };
            }
            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed) {
                return new LoginResponse { Success = false, ErrorMessage = "Invalid username or password" };
            }

            var token = CreateToken(user);

            return new LoginResponse { Success = true, Token = token };
        }

        public async Task<AddUserResponse> AddUserAsync(UserRequest request)
        {
            if (request.Password != request.ConfirmPassword)
            {
                return new AddUserResponse { Success = false, ErrorMessage = "Passwords do not match" };
            }

            var userExists = await UserExistsAsync(request.UserName);
            if (userExists) {
                return new AddUserResponse { Success = false, ErrorMessage = "Username already exists" };
            }

            var user = new AppUser
            {
                UserName = request.UserName,
            };
            user.PasswordHash = _hasher.HashPassword(user, request.Password);

            _userRepository.AddUser(user);
            await _userRepository.SaveAsync();
            return new AddUserResponse { Success = true };
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            var user = await _userRepository.GetUserByNameAsync(username);
            return user != null;
        }

        public string CreateToken(AppUser user)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
