using BookList.Api.Dtos;
using BookList.Api.Mappers;
using BookList.Api.Repositories;

namespace BookList.Api.Services
{
    public class QuoteService(IQuoteRepository quoteRepository, IUserService userService) : IQuoteService
    {
        private readonly IQuoteRepository _quoteRepository = quoteRepository;
        private readonly IUserService _userService = userService;

        public async Task<QuoteResponse?> AddQuoteAsync(int userId, QuoteRequest request)
        {
            var currentUser = await _userService.GetUserByIdAsync(userId);
            if (currentUser == null)
            {
                return null;
            }

            var quote = request.ToEntity(currentUser.Id);
            _quoteRepository.AddQuote(quote);
            await _quoteRepository.SaveAsync();
            return quote.ToResponse();
        }

        public async Task<UserQuotesResponse?> GetUserQuotesAsync(int userId)
        {
            var currentUser = await _userService.GetUserByIdAsync(userId);
            if (currentUser == null)
            {
                return null;
            }

            var quotes = await _quoteRepository.GetQuotesByUserIdAsync(currentUser.Id);
            if (quotes == null)
            {
                return new UserQuotesResponse { UserQuotes = [] };
            }

            var quotesList = quotes.Select(q => q.ToResponse()).ToList();
            return new UserQuotesResponse { UserQuotes = quotesList };
        }

        public async Task<QuoteResponse?> UpdateQuoteAsync(int id, QuoteRequest request)
        {
            var quote = await _quoteRepository.GetQuoteByIdAsync(id);
            if (quote == null)
            {
                return null;
            }

            quote.UpdateFromRequest(request);
            _quoteRepository.UpdateQuote(quote);
            await _quoteRepository.SaveAsync();
            return quote.ToResponse();
        }

        public async Task<QuoteResponse?> GetQuoteByIdAsync(int id)
        {
            var quote = await _quoteRepository.GetQuoteByIdAsync(id);
            if (quote == null)
            {
                return null;
            }

            return quote.ToResponse();
        }

        public async Task DeleteQuoteByIdAsync(int id)
        {
            var quote = await _quoteRepository.GetQuoteByIdAsync(id);
            if (quote == null)
            {
                return;
            }

            _quoteRepository.RemoveQuote(quote);
            await _quoteRepository.SaveAsync();
        }
    }
}
