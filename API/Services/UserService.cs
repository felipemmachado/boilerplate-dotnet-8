using Application.Common.Interfaces;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace API.Services;
public class UserService : IUserService
{
    private readonly string? Token;

    public UserService(IHttpContextAccessor accessor)
    {
        var authorization = accessor.HttpContext?.Request.Headers[HeaderNames.Authorization];

        if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
        {
            Token = authorization;
            var scheme = headerValue.Scheme;
            var parameter = headerValue.Parameter;

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(parameter);

            if (jsonToken is JwtSecurityToken token && token.Claims.First(claim => claim.Type == "aud").Value == "ApplicationToken")
            {
                UserId = new Guid(token.Claims.First(claim => claim.Type == "sub").Value);
                Roles = token.Claims.Where(claim => claim.Type == "role")?.Select(p => p.Value).ToArray() ?? [];
            }
        }
    }

    public Guid UserId { get; set; }
    public string[] Roles { get; set; }

    public string GetTokenAsync()
    {
        return Token ?? "";
    }

    public bool HaveAllRoles(string[] rolesCheck)
    {
        foreach (var role in rolesCheck)
        {
            foreach (var r in Roles)
            {
                if (!(r == role)) return false;
            }

        }
        return true;
    }

    public bool HaveSomeRole(string[] rolesCheck)
    {
        foreach (var userRole in rolesCheck)
        {
            return HaveSomeRole(userRole);
        }

        return false;
    }

    public bool HaveSomeRole(string roleCheck)
    {

        foreach (var r in Roles)
        {
            if (r == roleCheck) return true;
        }

        return false;
    }
}

