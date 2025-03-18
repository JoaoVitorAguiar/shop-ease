using System.Security.Claims;

namespace Authentication.Domain.Services;

public interface IJwtProvider
{
    string GenerateToken(Guid userId);
}