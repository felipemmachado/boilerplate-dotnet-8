using API.Dto;
using Application.Account.Command.ForgotPassword;
using Application.Account.Command.SignIn;
using Application.Account.Command.UpdatePassword;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.IdentityModel.Tokens.Jwt;


namespace API.Controllers.v1;

[AllowAnonymous]
public class AccountController(IJwtService jwtService) : BaseController
{
    private readonly IJwtService _jwtService = jwtService;

    [HttpPost("forgot-password")]
    public async Task ForgotPassword(ForgotPasswordCommand command)
    {
        await Mediator.Send(command);
    }



    [HttpPost("sign-in")]
    public async Task<ActionResult<SignInDto>> SignIn(SignInCommand command)
    {
        var response = await Mediator.Send(command);

        return Ok(response);
    }


    [HttpPost("new-password")]
    public async Task<ActionResult> CreateNewPassword(NewPasswordDto newPasswordDto)
    {
        var isValid = await _jwtService.ValidPasswordToken(newPasswordDto.Token);
        if (!isValid) throw new ValidationException("Token", "Token Expirado.");

        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(newPasswordDto.Token);

        if (jsonToken is JwtSecurityToken token)
        {
            var userId = new Guid(token.Claims.First(claim => claim.Type == "sub").Value);

            var command = new UpdatePasswordCommand()
            {
                Password = newPasswordDto.Password,
                RePassword = newPasswordDto.RePassword,
                UserId = userId,
            };

            return Ok(await Mediator.Send(command));
        }

        return Unauthorized();
    }
}

