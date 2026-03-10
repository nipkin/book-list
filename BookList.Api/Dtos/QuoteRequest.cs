using System.ComponentModel.DataAnnotations;

namespace BookList.Api.Dtos
{
    public class QuoteRequest
    {
        [Required(ErrorMessage = "Quote cannot be empty")]
        public string Text { get; set; } = string.Empty;
    }
}