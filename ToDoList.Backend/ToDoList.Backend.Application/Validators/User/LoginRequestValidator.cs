using FluentValidation;
using ToDoList.Domain.Request;

namespace ToDoList.Application.Validators.User;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotNull().WithMessage("Email is null")
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("You entered an incorrect email format")
            .Must(email => !email.Contains(' ')).WithMessage("Email must not contain spaces.");

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("The password must not be empty.")
            .Must(password => !password.Contains(' ')).WithMessage("The password must not contain spaces.");
    }
}