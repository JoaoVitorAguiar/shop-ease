using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authentication.Domain.Services;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Infrastructure.Services;

public class JwtProvider(string secretKey, string issuer, string audience, int expiryInMinutes)
    : IJwtProvider
{
    private readonly string _secretKey = secretKey; 
    private readonly string _issuer = issuer;   
    private readonly string _audience = audience;  
    private readonly int _expiryInMinutes = expiryInMinutes;

    public string GenerateToken(Guid userId)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_expiryInMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateToken(IEnumerable<Claim> claims)
    {
        throw new NotImplementedException();
    }
}