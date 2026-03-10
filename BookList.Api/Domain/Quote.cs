namespace BookList.Api.Domain
{
    public class Quote
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        
        public int AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
