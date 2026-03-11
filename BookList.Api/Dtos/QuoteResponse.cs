namespace BookList.Api.Dtos
{
    public class QuoteResponse
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
    }
}
