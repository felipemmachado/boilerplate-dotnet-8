using API.Dto;
using Application.Common.Interfaces;
using Application.UseCases.Account.UpdatePassword;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1;

public class MeController(IUserService userService) : BaseController
{
    private readonly IUserService _userService = userService;

    [HttpPut("password")]
    public async Task UpdatePassword(UpdateActualPasswordDto userCommand)
    {
        var command = new UpdatePasswordCommand()
        {
            RePassword = userCommand.RePassword,
            Password = userCommand.Password,
            UserId = _userService.UserId,
            ActualPassword = userCommand.ActualPassword,
        };

        await Mediator.Send(command);
    }
}

