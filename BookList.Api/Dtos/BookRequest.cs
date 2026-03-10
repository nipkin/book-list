using System.ComponentModel.DataAnnotations;

namespace BookList.Api.Dtos
{
    public class BookRequest
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; }
    }
}