using FluentValidation;

namespace Application.Account.Command.ForgotPassword;
public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordValidator()
    {
        RuleFor(v => v.Email)
            .EmailAddress()
            .WithMessage("E-mail inválido.");
    }
}

