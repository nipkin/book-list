using BookList.Api.Data;
using BookList.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookList.Api.Repositories
{
    public class UserRepository(BookListDbContext bookListDbContext) : IUserRepository
    {
        private readonly BookListDbContext _bookListDbContext = bookListDbContext;

        public void AddUser(AppUser user)
        {
            _bookListDbContext.AppUsers.Add(user);
        }

        public void RemoveUser(AppUser user)
        {
            _bookListDbContext.AppUsers.Remove(user);
        }

        public void UpdateUser(AppUser user)
        {
            _bookListDbContext.AppUsers.Update(user);
        }

        public async Task<AppUser?> GetUserByIdAsync(int id)
        {
            return await _bookListDbContext.AppUsers.FindAsync(id);
        }

        public async Task<AppUser?> GetUserByNameAsync(string username)
        {
            return await _bookListDbContext.AppUsers.FirstOrDefaultAsync(x => x.Username == username.ToString());
        }

        public async Task SaveAsync()
        {
            await _bookListDbContext.SaveChangesAsync();
        }
    }
}
