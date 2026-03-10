using BookList.Api.Domain;

namespace BookList.Api.Repositories
{
    public interface IBookRepository
    {
        void AddBook(Book book);
        void RemoveBook(Book book);
        void UpdateBook(Book book);
        Task<Book?> GetBookByIdAsync(int id);
        Task<List<Book>> GetAllBooksAsync();
        Task SaveAsync();
    }
}
