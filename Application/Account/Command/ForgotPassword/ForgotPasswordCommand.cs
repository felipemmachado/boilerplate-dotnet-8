using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Account.Command.ForgotPassword;
public record struct ForgotPasswordCommand(string Email) : IRequest<Unit> { }

public class ForgotPasswordCommandHandler(
    IApplicationDbContext context,
    IEmailService emailService,
    IJwtService jwtService
    ) : IRequestHandler<ForgotPasswordCommand, Unit>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IEmailService _emailService = emailService;
    private readonly IJwtService _jwtService = jwtService;

    public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _context
            .Users
            .FirstOrDefaultAsync(p => p.Email == request.Email, cancellationToken: cancellationToken);

        if (user == null)
            return Unit.Value;

        var changePasswordId = Guid.NewGuid();

        var token = _jwtService.PasswordToken(
            user.Id.ToString(),
            user.Email);

        var link = $"alterar-senha/?token={token}";

        var success = await _emailService.ForgotPassword(user.Email, user.Name.Split(" ")[0], link);

        if (success)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        else
        {
            throw new ValidationException("Usuário", "Não foi possível aceitar sua solicitação, tente novamente mais tarde.");
        }

        return Unit.Value;
    }
}

