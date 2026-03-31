using BookList.Api.Dtos;
using BookList.Api.Mappers;
using BookList.Api.Repositories;

namespace BookList.Api.Services
{
    public class QuoteService(IQuoteRepository quoteRepository, IUserService userService) : IQuoteService
    {
        public async Task<QuoteResponse?> AddQuoteAsync(int userId, QuoteRequest request)
        {
            var currentUser = await userService.GetUserByIdAsync(userId);
            if (currentUser == null)
            {
                return null;
            }

            var quote = request.ToEntity(currentUser.Id);
            quoteRepository.AddQuote(quote);
            await quoteRepository.SaveAsync();
            return quote.ToResponse();
        }

        public async Task<UserQuotesResponse?> GetUserQuotesAsync(int userId)
        {
            var currentUser = await userService.GetUserByIdAsync(userId);
            if (currentUser == null)
            {
                return null;
            }

            var quotes = await quoteRepository.GetQuotesByUserIdAsync(currentUser.Id);
            if (quotes == null)
            {
                return new UserQuotesResponse { UserQuotes = [] };
            }

            var quotesList = quotes.Select(q => q.ToResponse()).ToList();
            return new UserQuotesResponse { UserQuotes = quotesList };
        }

        public async Task<QuoteResponse?> UpdateQuoteAsync(int id, QuoteRequest request)
        {
            var quote = await quoteRepository.GetQuoteByIdAsync(id);
            if (quote == null)
            {
                return null;
            }

            quote.UpdateFromRequest(request);
            quoteRepository.UpdateQuote(quote);
            await quoteRepository.SaveAsync();
            return quote.ToResponse();
        }

        public async Task<QuoteResponse?> GetQuoteByIdAsync(int id)
        {
            var quote = await quoteRepository.GetQuoteByIdAsync(id);
            if (quote == null)
            {
                return null;
            }

            return quote.ToResponse();
        }

        public async Task<bool> DeleteQuoteByIdAsync(int id)
        {
            var quote = await quoteRepository.GetQuoteByIdAsync(id);
            if (quote == null)
            {
                return false;
            }

            quoteRepository.RemoveQuote(quote);
            await quoteRepository.SaveAsync();
            return true;
        }
    }
}
