namespace BookList.Api.Domain
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public ICollection<Quote>? Quotes { get; set; }
    }
}
