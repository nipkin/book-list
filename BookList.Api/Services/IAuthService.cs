using BookList.Api.Domain;
using BookList.Api.Dtos;

namespace BookList.Api.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> AuthUserAsync(LoginRequest request);
        Task<AddUserResponse> AddUserAsync(UserRequest request);
        string CreateToken(AppUser user);
        Task<bool> UserExistsAsync(string username);
    }
}
