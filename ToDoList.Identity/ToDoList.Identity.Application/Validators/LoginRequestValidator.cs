using FluentValidation;
using ToDoList.Identity.Domain.Requests;

namespace ToDoList.Identity.Application.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("The email address format is incorrect.")
            .Must(email => !email.Contains(' ')).WithMessage("Email must not contain spaces.");

        RuleFor(user => user.Password)
                .NotEmpty().WithMessage("The password cannot be empty.")
            .MinimumLength(6).WithMessage("The password must contain at least 6 characters.")
            .Must(password => !password.Contains(' ')).WithMessage("The password must not contain spaces.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$")
            .WithMessage("The password must contain at least one number, one uppercase letter and one lowercase letter.");
    }
}