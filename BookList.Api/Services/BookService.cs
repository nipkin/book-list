using BookList.Api.Dtos;
using BookList.Api.Repositories;
using BookList.Api.Mappers;

namespace BookList.Api.Services
{
    public class BookService(IBookRepository bookRepository) : IBookService
    {
        public async Task<BookResponse?> AddBookAsync(BookRequest request)
        {
            var book = request.ToEntity();
            bookRepository.AddBook(book);
            await bookRepository.SaveAsync();
            return book.ToResponse();
        }

        public async Task<BookResponse?> UpdateBookAsync(int id, BookRequest request)
        {
            var book = await bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return null;
            }
            book.UpdateFromRequest(request);
            bookRepository.UpdateBook(book);
            await bookRepository.SaveAsync();
            return book.ToResponse();
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return false;
            }
            bookRepository.RemoveBook(book);
            await bookRepository.SaveAsync();
            return true;
        }

        public async Task<BookResponse?> GetBookByIdAsync(int id)
        {
            var book = await bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return null;
            }
            return book.ToResponse();
        }

        public async Task<IEnumerable<BookResponse>> GetAllBooksAsync()
        {
            var books = await bookRepository.GetAllBooksAsync();
            return books.Select(b => b.ToResponse()).ToList();
        }
    }
}
