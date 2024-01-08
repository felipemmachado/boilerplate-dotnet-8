using Application.Common.Authorization;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Account.SignIn;
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
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Email == request.Email, cancellationToken: cancellationToken)
            ??
            throw new ValidationException("Email ou senha", "E-mail e/ou senha inválidos.");

        if (user.IsDisabled())
            throw new ValidationException("Email ou senha", "Usuário desativado.");


        if (!_passwordService.Check(user.PasswordHash, request.Password))
            throw new ValidationException("Email ou senha", "E-mail e/ou senha inválidos.");


        user.UpdateLastAccess();

        if (user.FirstAccess == null)
        {
            user.UpdateFirstAccess();
            user.UpdateLastAccess();
        }

        var profile = await _context
                .Profiles
                .Where(p => p.Id == user.ProfileId)
                .Select(p => new { p.IsAdmin, p.Roles })
                .FirstOrDefaultAsync(cancellationToken)
                ??
                throw new ValidationException("Perfil", "Nenhum perfil encontrado."); ;

        var roles = profile.IsAdmin ? Roles.AllRoles.Select(p => p.Value).ToArray() : profile.Roles;

        var token = _jwtService.ApplicationAccessToken(user.Id.ToString(), roles);

        await _context.SaveChangesAsync(cancellationToken);

        return new SignInDto
        {
            AccessToken = token,
            TemporaryPassword = user.RequestChangePassword
        };
    }
}

