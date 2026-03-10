using BookList.Api.Data;
using BookList.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookList.Api.Repositories
{
    public class BookRepository(BookListDbContext bookListDbContext) : IBookRepository
    {
        private readonly BookListDbContext _bookListDbContext = bookListDbContext;

        public void AddBook(Book book)
        {
            _bookListDbContext.Books.Add(book);
        }

        public void RemoveBook(Book book)
        {
            _bookListDbContext.Books.Remove(book);
        }

        public void UpdateBook(Book book)
        {
            _bookListDbContext.Books.Update(book);
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _bookListDbContext.Books.FindAsync(id);
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _bookListDbContext.Books.ToListAsync();
        }

        public async Task SaveAsync()
        {
           await _bookListDbContext.SaveChangesAsync();
        }
    }
}
