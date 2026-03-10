using BookList.Api.Domain;
using BookList.Api.Dtos;

namespace BookList.Api.Mappers
{
    public static class BookMapper
    {
        public static Book ToEntity(this BookRequest request)
        {
            return new Book
            {
                Author = request.Author,
                Title = request.Title,
                PublicationDate = request.PublicationDate
            };
        }

        public static BookResponse ToResponse(this Book book)
        {
            return new BookResponse
            {
                Id = book.Id,
                Author = book.Author,
                Title = book.Title,
                PublicationDate = book.PublicationDate
            };
        }

        public static void UpdateFromRequest(this Book book, BookRequest request)
        {
            book.Author = request.Author;
            book.Title = request.Title;
            book.PublicationDate = request.PublicationDate;
        }
    }
}