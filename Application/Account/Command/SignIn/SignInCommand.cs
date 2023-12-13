using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Account.Command.SignIn;
public record struct SignInCommand(
    string Email,
    string Password) : IRequest<SignInDto>
{ }

public class SignInCommandHandler(
    IApplicationDbContext context,
    IPasswordService passwordService,
    IJwtService jwtService) : IRequestHandler<SignInCommand, SignInDto>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IJwtService _jwtService = jwtService;
    private readonly IPasswordService _passwordService = passwordService;

    public async Task<SignInDto> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _context
            .Users
            .FirstOrDefaultAsync(p => p.Email == request.Email, cancellationToken: cancellationToken)
            ??
            throw new ValidationException("Email ou senha", "E-mail e/ou senha inválidos.");

        if (user.IsDisabled())
            throw new ValidationException("Email ou senha", "Usuário desativado.");


        if (!_passwordService.Check(user.Password, request.Password))
            throw new ValidationException("Email ou senha", "E-mail e/ou senha inválidos.");


        user.UpdateLastAccess(DateTime.UtcNow);

        if (user.FirstAccess == null)
        {
            user.UpdateFirstAccess(DateTime.UtcNow);
            user.UpdateLastAccess(user.FirstAccess ?? DateTime.UtcNow);
        }

        await _context.SaveChangesAsync(cancellationToken);

        var token = "";

        token = _jwtService.ApplicationAccessToken(user.Id.ToString(), user.Roles.ToArray());

        return new SignInDto
        {
            AccessToken = token,
            TemporaryPassword = user.ForceChangePassword
        };
    }
}

