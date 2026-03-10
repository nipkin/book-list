using BookList.Api.Domain;

namespace BookList.Api.Repositories
{
    public interface IUserRepository
    {
        void AddUser(AppUser user);
        void RemoveUser(AppUser user);
        void UpdateUser(AppUser user);
        Task<AppUser?> GetUserByIdAsync(int id);
        Task<AppUser?> GetUserByNameAsync(string userName);
        Task SaveAsync();
    }
}
