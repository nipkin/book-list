using System.Security.Claims;

namespace BookList.Api.Helpers
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool TryGetUserId(this ClaimsPrincipal user, out int userId)
        {
            userId = 0;

            var userIdString = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
                return false;

            return int.TryParse(userIdString, out userId);
        }
    }
}
