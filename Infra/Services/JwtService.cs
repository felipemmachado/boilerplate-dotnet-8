using Application.Common.Configs;
using Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infra.Services;
public class JwtService(IOptions<JwtPasswordConfig> jwtPasswordConfig, IOptions<JwtApplicationConfig> jwtApplicationConfig) : IJwtService
{
    private readonly JwtApplicationConfig _jwtApplicationConfig = jwtApplicationConfig.Value
        ?? throw new ArgumentNullException(nameof(jwtApplicationConfig));

    private readonly JwtPasswordConfig _jwtPasswordConfig = jwtPasswordConfig.Value
        ?? throw new ArgumentNullException(nameof(jwtPasswordConfig));

    public string ApplicationAccessToken(string userId, IEnumerable<string> roles)
    {
        var key = Encoding.UTF8.GetBytes(_jwtApplicationConfig.Key);

        IList<Claim> claimCollection = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Sub, userId),
        };

        foreach (var role in roles)
            claimCollection.Add(new Claim(ClaimTypes.Role, role));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claimCollection),
            Expires = DateTime.UtcNow.AddMinutes(_jwtApplicationConfig.ExpiresInMinutes),
            Issuer = _jwtApplicationConfig.Issuer,
            Audience = _jwtApplicationConfig.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var stringToken = tokenHandler.WriteToken(token);

        return stringToken;
    }

    public string PasswordToken(string userId, string name)
    {
        var key = Encoding.UTF8.GetBytes(_jwtPasswordConfig.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Name, name),
            }),

            Expires = DateTime.UtcNow.AddMinutes(_jwtPasswordConfig.ExpiresInMinutes),
            Issuer = _jwtPasswordConfig.Issuer,
            Audience = _jwtPasswordConfig.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var stringToken = tokenHandler.WriteToken(token);

        return stringToken;
    }


    public async Task<bool> ValidPasswordToken(string token)
    {
        var key = Encoding.UTF8.GetBytes(_jwtPasswordConfig.Key);

        var handler = new JwtSecurityTokenHandler();
        var result = await handler.ValidateTokenAsync(token, new TokenValidationParameters()
        {
            ValidIssuer = _jwtPasswordConfig.Issuer,
            ValidAudience = _jwtPasswordConfig.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        });

        return result.IsValid;
    }
}

