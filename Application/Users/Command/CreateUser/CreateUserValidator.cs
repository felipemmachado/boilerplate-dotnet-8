using FluentValidation;

namespace Application.Users.Command.CreateUser;
public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {

        RuleFor(v => v.Name)
            .MaximumLength(100)
            .NotEmpty()
            .WithMessage("Você tem que digitar um nome.");

        RuleFor(v => v.Email)
            .EmailAddress()
            .WithMessage("E-mail inválido.");

    }
}

