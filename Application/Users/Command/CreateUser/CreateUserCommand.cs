using Application.Account.Queries.ExistsEmail;
using Application.Common.Authorization;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Application.Users.Command.CreateUser;

public record struct CreateUserCommand(
    Guid ProfileId,
    string Name,
    string Email,
    IEnumerable<string> Roles
    ) : IRequest<CreateUserDto>
{ }

[Authorize(Roles = Roles.Users)]
public class CreateUserCommandHandler(
    IApplicationDbContext context,
    IPasswordService passwordService,
    IMediator mediator
    ) : IRequestHandler<CreateUserCommand, CreateUserDto>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMediator _mediator = mediator;
    private readonly IPasswordService _passwordService = passwordService;

    public async Task<CreateUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ExistsEmailQuery()
        {
            Email = request.Email
        }, cancellationToken);

        if (result.Exists)
        {
            throw new ValidationException("Usuário", "Esse usuário já existe.");
        }

        var password = _passwordService.GetAlphanumericCode(8);
        var user = new User(request.ProfileId, request.Name, request.Email, _passwordService.Generate(password, false));

        _context.Users.Add(user);

        await _context.SaveChangesAsync(cancellationToken);

        return new CreateUserDto()
        {
            Id = user.Id,
            TemporaryPassword = password,
        };
    }
}

