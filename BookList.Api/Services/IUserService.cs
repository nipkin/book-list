using BookList.Api.Dtos;

namespace BookList.Api.Services
{
    public interface IUserService
    {
        Task<UserResponse?> GetUserByIdAsync(int id);
    }
}