using BookList.Api.Dtos;
using BookList.Api.Repositories;
using BookList.Api.Mappers;

namespace BookList.Api.Services
{
    public class BookService(IBookRepository bookRepository) : IBookService
    {
        private readonly IBookRepository _bookRepository = bookRepository;

        public async Task<BookResponse?> AddBookAsync(BookRequest request)
        {
            var book = request.ToEntity();
            _bookRepository.AddBook(book);
            await _bookRepository.SaveAsync();
            return book.ToResponse();
        }

        public async Task<BookResponse?> UpdateBookAsync(int id, BookRequest request)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return null;
            }
            book.UpdateFromRequest(request);
            _bookRepository.UpdateBook(book);
            await _bookRepository.SaveAsync();
            return book.ToResponse();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return;
            }
            _bookRepository.RemoveBook(book);
            await _bookRepository.SaveAsync();
        }

        public async Task<BookResponse?> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return null;
            }
            return book.ToResponse();
        }

        public async Task<List<BookResponse>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return books.Select(b => b.ToResponse()).ToList();
        }
    }
}
