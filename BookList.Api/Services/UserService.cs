using BookList.Api.Dtos;
using BookList.Api.Repositories;
using BookList.Api.Mappers;

namespace BookList.Api.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public async Task<UserResponse?> GetUserByIdAsync(int id)
        {
            var user = await userRepository.GetUserByIdAsync(id);
            if (user == null) { 
                return null;
            }
            return user?.ToResponse();
        }
    }
}