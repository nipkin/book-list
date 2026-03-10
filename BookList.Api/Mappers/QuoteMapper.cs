using BookList.Api.Domain;
using BookList.Api.Dtos;

namespace BookList.Api.Mappers
{
    public static class QuoteMapper
    {
        public static Quote ToEntity(this QuoteRequest request, int userId)
        {
            return new Quote
            {
                Text = request.Text,
                AppUserId = userId
            };
        }

        public static QuoteResponse ToResponse(this Quote quote)
        {
            return new QuoteResponse
            {
                Id = quote.Id,
                UserId = quote.AppUserId,
                Text = quote.Text
            };
        }

        public static void UpdateFromRequest(this Quote quote, QuoteRequest request)
        {
            quote.Text = request.Text;
        }
    }
}
