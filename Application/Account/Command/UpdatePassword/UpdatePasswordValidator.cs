using FluentValidation;

namespace Application.Account.Command.UpdatePassword;
public class UpdatePasswrodPasswordValidator : AbstractValidator<UpdatePasswordCommand>
{
    public UpdatePasswrodPasswordValidator()
    {
        RuleFor(v => v.UserId)
            .NotEmpty()
            .NotNull()
            .WithMessage("O id do usuário é obrigatório.");


        RuleFor(v => v.Password)
            .NotEmpty()
            .WithMessage("Você tem que digitar uma senha.");

        RuleFor(v => v.Password).Equal(p => p.RePassword)
            .WithMessage("As senhas não são iguais.");
    }
}

