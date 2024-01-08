using FluentValidation;

namespace Application.Queries.Users.ExistsUser;

public class ExistsUserValidator : AbstractValidator<ExistsUserQuery>
{
    public ExistsUserValidator()
    {
        RuleFor(p => p.Email).EmailAddress().NotEmpty().NotNull().WithMessage("O e-mail é obrigatório");
    }
}

