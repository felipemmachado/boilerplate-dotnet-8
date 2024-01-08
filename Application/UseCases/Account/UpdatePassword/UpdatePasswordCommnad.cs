using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Account.UpdatePassword;

public record struct UpdatePasswordCommand(
    Guid UserId,
    string? ActualPassword,
    string Password,
    string RePassword) : IRequest<Unit>
{ }

public class UpdatePasswordCommandHandler(
    IApplicationDbContext context,
    IPasswordService passwordService
    ) : IRequestHandler<UpdatePasswordCommand, Unit>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IPasswordService _passwordService = passwordService;

    public async Task<Unit> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        var newPasswordHash = _passwordService.Hash(request.Password);

        var user = await _context
            .Users
            .FirstOrDefaultAsync(p =>
                p.Id == request.UserId, cancellationToken)
            ?? throw new NotFoundException("Usuário", "Usuário não encontrado.");


        if (!string.IsNullOrWhiteSpace(request.ActualPassword))
        {
            user.ChangePassword();
            if (!_passwordService.Verify(user.PasswordHash, request.ActualPassword))
                throw new ValidationException("Password", "Senha atual inválida.");
        }

        user.UpdatePasswordHash(newPasswordHash);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

