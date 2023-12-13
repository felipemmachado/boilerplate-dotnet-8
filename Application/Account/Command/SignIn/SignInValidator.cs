using FluentValidation;

namespace Application.Account.Command.SignIn;
public class SignInValidator : AbstractValidator<SignInCommand>
{
    public SignInValidator()
    {
        RuleFor(v => v.Email)
            .EmailAddress()
            .WithMessage("E-mail inválido.");

        RuleFor(v => v.Password)
            .NotEmpty()
            .WithMessage("Você tem que digitar uma senha.");
    }
}

