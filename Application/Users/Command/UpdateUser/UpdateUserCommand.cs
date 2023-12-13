using Application.Common.Authorization;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Command.UpdateUser;

public record struct UpdateUserCommand(
    Guid UserId,
    IEnumerable<string> Roles
) : IRequest<Unit>
{ }

[Authorize(Roles = Role.Users)]
public class UpdateUserCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context
                            .Users
                            .Where(p => p.Id == request.UserId)
                            .FirstOrDefaultAsync(cancellationToken) ?? throw new ValidationException("Usuário", "Usuário não encontrado");

        user.UpdateRoles(request.Roles);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

