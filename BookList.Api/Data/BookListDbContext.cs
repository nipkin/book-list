using BookList.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookList.Api.Data
{
    public class BookListDbContext(DbContextOptions<BookListDbContext> options) : DbContext(options)
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
