using BookList.Api.Dtos;

namespace BookList.Api.Services
{
    public interface IBookService
    {
        Task<BookResponse?> AddBookAsync(BookRequest request);
        Task<BookResponse?> UpdateBookAsync(int id, BookRequest request);
        Task<BookResponse?> GetBookByIdAsync(int id);
        Task<IEnumerable<BookResponse>> GetAllBooksAsync();
        Task<bool> DeleteBookAsync(int id);
    }
}
