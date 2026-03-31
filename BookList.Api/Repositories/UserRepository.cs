using BookList.Api.Data;
using BookList.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookList.Api.Repositories
{
    public class UserRepository(BookListDbContext bookListDbContext) : IUserRepository
    {
        public void AddUser(AppUser user)
        {
            bookListDbContext.AppUsers.Add(user);
        }

        public void RemoveUser(AppUser user)
        {
            bookListDbContext.AppUsers.Remove(user);
        }

        public void UpdateUser(AppUser user)
        {
            bookListDbContext.AppUsers.Update(user);
        }

        public async Task<AppUser?> GetUserByIdAsync(int id)
        {
            return await bookListDbContext.AppUsers.FindAsync(id);
        }

        public async Task<AppUser?> GetUserByNameAsync(string username)
        {
            return await bookListDbContext.AppUsers.FirstOrDefaultAsync(x => x.Username == username.ToString());
        }

        public async Task SaveAsync()
        {
            await bookListDbContext.SaveChangesAsync();
        }
    }
}
