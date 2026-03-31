using BookList.Api.Data;
using BookList.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookList.Api.Repositories
{
    public class BookRepository(BookListDbContext bookListDbContext) : IBookRepository
    {
        public void AddBook(Book book)
        {
            bookListDbContext.Books.Add(book);
        }

        public void RemoveBook(Book book)
        {
            bookListDbContext.Books.Remove(book);
        }

        public void UpdateBook(Book book)
        {
            bookListDbContext.Books.Update(book);
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await bookListDbContext.Books.FindAsync(id);
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await bookListDbContext.Books.ToListAsync();
        }

        public async Task SaveAsync()
        {
           await bookListDbContext.SaveChangesAsync();
        }
    }
}
