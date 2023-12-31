﻿using API.Dto;
using Application.Account.Command.UpdatePassword;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

