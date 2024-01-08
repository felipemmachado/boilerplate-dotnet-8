using Application.Common.Authorization;
using Application.Common.Constants;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Queries.Users.ExistsUser;
using Application.Queries.Users.GetUsers;
using Application.UseCases.Users.CreateUser;
using Application.UseCases.Users.UpdateUser;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1;

public class UserController(IUserService userService) : BaseController
{
    private readonly IUserService _userService = userService;

    [HttpGet]
    public async Task<IEnumerable<UserDto>> GetUsers()
    {
        //var hasUsers = _userService.HaveSomeRole(Roles.Users);
        // if (!hasUsers) throw new UnauthorizedAccessException("Você não tem permissão.");
        return await Mediator.Send(new GetUsersQuery());
    }


    [HttpGet("{email}/exists")]
    public async Task<ExistsUserDto> ExistsUser(string email)
    {
        var query = new ExistsUserQuery(email);

        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<CreateUserDto> Create(CreateUserCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPatch("{id}")]
    public async Task Update(Guid id, [FromBody] UpdateUserCommand command)
    {
        if (id != command.UserId) throw new NotFoundException(ApiResponseMessages.UserNotFound);

        await Mediator.Send(command);
    }
}

