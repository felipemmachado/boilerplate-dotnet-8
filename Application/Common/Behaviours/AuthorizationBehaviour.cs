using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;

namespace Application.Common.Behaviours;
public class AuthorizationBehaviour<TRequest, TResponse>(
    IUserService userService) :
    IPipelineBehavior<TRequest, TResponse> where TRequest :
    IRequest<TResponse>
{
    private readonly IUserService _userService = userService;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

        if (authorizeAttributes.Any())
        {
            // Must be authenticated user
            if (_userService?.UserId == null) throw new UnauthorizedAccessException();

            // Role-based authorization
            var authorizeAttributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));

            if (authorizeAttributesWithRoles.Any())
            {
                foreach (var roles in authorizeAttributesWithRoles.Select(a => a.Roles?.Split(',')))
                {
                    if (roles == null) continue;

                    var authorized = _userService.HaveSomeRole(roles);

                    // Must be a member of at least one role in roles
                    if (!authorized) throw new ForbiddenAccessException();
                }
            }
        }

        return await next();
    }
}

