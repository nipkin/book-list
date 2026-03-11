using System.ComponentModel.DataAnnotations;

namespace BookList.Api.Dtos
{
    public class UserRequest
    {
        [Required(ErrorMessage = "Title is required")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Confirm password cannot be empty")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
