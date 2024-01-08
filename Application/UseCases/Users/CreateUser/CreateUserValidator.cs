using Application.Common.Constants;
using FluentValidation;

namespace Application.UseCases.Users.CreateUser;
public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {

        RuleFor(v => v.Name)
            .MaximumLength(100)
            .NotEmpty()
            .WithMessage(ApiResponseMessages.RequiredName);

        RuleFor(v => v.Email)
            .EmailAddress()
            .WithMessage(ApiResponseMessages.InvalidEmail);

    }
}

