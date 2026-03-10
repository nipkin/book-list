using BookList.Api.Dtos;

namespace BookList.Api.Services
{
    public interface IQuoteService
    {
        Task<QuoteResponse?> GetQuoteByIdAsync(int id);
        Task<QuoteResponse?> AddQuoteAsync(int userId, QuoteRequest request);
        Task<UserQuotesResponse?> GetUserQuotesAsync(int userId);
        Task<QuoteResponse?> UpdateQuoteAsync(int id, QuoteRequest request);
        Task DeleteQuoteByIdAsync(int id);
    }
}
