using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace aspCrud.Services.TokenService;

public class TokenService: ITokenService
{
    private readonly TokenOptions _options;

    public TokenService(IOptions<TokenOptions> options)
    {
        _options = options.Value;
    }
    
    public LoginResponseDTO GenerateToken(Guid id, string email)
    {
        // Header
        var _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
        var signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var header = new JwtHeader(signingCredentials);
        
        // Claims
        var claims = new[]
        {
            new Claim("Email", email),
            new Claim("Id", id.ToString())
        };

        var currentTime = DateTime.UtcNow;
        var expiredTime = currentTime.AddDays(1);
        
        // Payload
        var payload = new JwtPayload
        (
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            notBefore: currentTime,
            expires: expiredTime
        );
        
        // Token
        var token = new JwtSecurityToken(header, payload);
        var dataToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new LoginResponseDTO
        {
            Id = id,
            Token = dataToken,
            ExpireDateTime = expiredTime
        };
    }
}