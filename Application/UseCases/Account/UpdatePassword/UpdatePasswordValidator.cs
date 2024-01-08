using Application.Common.Constants;
using FluentValidation;

namespace Application.UseCases.Account.UpdatePassword;
public class UpdatePasswrodPasswordValidator : AbstractValidator<UpdatePasswordCommand>
{
    public UpdatePasswrodPasswordValidator()
    {
        RuleFor(v => v.Password)
            .NotEmpty()
            .WithMessage(ApiResponseMessages.RequiredPassword);

        RuleFor(v => v.Password).Equal(p => p.RePassword)
            .WithMessage(ApiResponseMessages.PasswordsAreNotTheSame);
    }
}

