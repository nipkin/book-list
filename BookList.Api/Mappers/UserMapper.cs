using BookList.Api.Domain;
using BookList.Api.Dtos;

namespace BookList.Api.Mappers
{
    public static class UserMapper
    {
        public static UserResponse ToResponse(this AppUser user)
        {
            return new UserResponse
            {
                Id = user.Id,
                Username = user.UserName
            };
        }
    }
}
