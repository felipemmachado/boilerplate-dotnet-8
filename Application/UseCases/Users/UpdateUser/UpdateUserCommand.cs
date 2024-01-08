using Application.Common.Authorization;
using Application.Common.Constants;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Users.UpdateUser;

public record struct UpdateUserCommand(
    Guid UserId,
    Guid ProfileId
) : IRequest<Unit>
{ }

[Authorize(Roles = Roles.Users)]
public class UpdateUserCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context
                            .Users
                            .Where(p => p.Id == request.UserId)
                            .FirstOrDefaultAsync(cancellationToken) 
                            ?? 
                            throw new ValidationException(ApiResponseMessages.User, ApiResponseMessages.UserNotFound);

        user.ChangeProfile(request.ProfileId);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

