using Microsoft.EntityFrameworkCore;
using BookList.Api.Data;
using BookList.Api.Domain;

namespace BookList.Api.Repositories
{
    public class QuoteRepository(BookListDbContext bookListDbContext) : IQuoteRepository
    {
        public void AddQuote(Quote quote)
        {
            bookListDbContext.Quotes.Add(quote);
        }

        public void RemoveQuote(Quote quote)
        {
            bookListDbContext.Quotes.Remove(quote);
        }

        public void UpdateQuote(Quote quote)
        {
            bookListDbContext.Quotes.Update(quote);
        }

        public async Task SaveAsync()
        {
            await bookListDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Quote>?> GetQuotesByUserIdAsync(int id)
        {
            var quotes = await bookListDbContext.Quotes.Where(q => q.AppUserId == id).ToListAsync();
            if (quotes.Count <= 0) {
                return null;
            }

            return quotes;
        }

        public async Task<Quote?> GetQuoteByIdAsync(int id)
        {
            return await bookListDbContext.Quotes.FindAsync(id);
        }
    }
}
