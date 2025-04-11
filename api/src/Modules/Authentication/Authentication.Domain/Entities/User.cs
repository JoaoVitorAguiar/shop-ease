using System.Security.Claims;

namespace Authentication.Domain.Entities;

public class User(ClaimsPrincipal claimsPrincipal)
{
    private readonly ClaimsPrincipal _claimsPrincipal = claimsPrincipal ?? throw new ArgumentNullException(nameof(claimsPrincipal));

    public Guid Id
    {
        get
        {
            var userIdClaim = _claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                throw new UnauthorizedAccessException("User ID not found or invalid.");
            }

            return userId;
        }
    }
}