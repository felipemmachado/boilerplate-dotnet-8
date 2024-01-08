using Application.Common.Constants;
using FluentValidation;

namespace Application.Queries.Account.ExistsEmail;
public class ExistsEmailValidator : AbstractValidator<ExistsEmailQuery>
{
    public ExistsEmailValidator()
    {
        RuleFor(p => p.Email)
            .EmailAddress()
            .NotEmpty()
            .NotNull()
            .WithMessage(ApiResponseMessages.RequiredEmail);
    }
}

