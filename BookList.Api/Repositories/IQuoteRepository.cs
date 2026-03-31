using BookList.Api.Domain;

namespace BookList.Api.Repositories
{
    public interface IQuoteRepository
    {
        void AddQuote(Quote quote);
        void RemoveQuote(Quote quote);
        void UpdateQuote(Quote quote);
        Task<IEnumerable<Quote>?> GetQuotesByUserIdAsync(int id);
        Task<Quote?> GetQuoteByIdAsync(int id);
        Task SaveAsync();
    }
}
