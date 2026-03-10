using BookList.Api.Dtos;
using BookList.Api.Repositories;
using BookList.Api.Mappers;

namespace BookList.Api.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<UserResponse?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) { 
                return null;
            }
            return user?.ToResponse();
        }
    }
}