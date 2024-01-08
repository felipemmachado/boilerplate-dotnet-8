using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace API.Services;
public class UserService : IUserService
{
    public UserService(IHttpContextAccessor accessor)
    {
        var userId = accessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var roels = accessor.HttpContext?.User.FindAll(ClaimTypes.Role)?.Select(p => p.Value).ToArray();

        if (userId != null)
        {
            UserId = new Guid(userId);
            Roles = accessor.HttpContext?.User.FindAll(ClaimTypes.Role)?.Select(p => p.Value).ToArray() ?? [];
        }
    }

    public Guid UserId { get; set; }
    public string[] Roles { get; set; } = [];


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


