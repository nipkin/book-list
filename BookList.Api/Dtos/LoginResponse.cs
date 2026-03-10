namespace BookList.Api.Dtos
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}