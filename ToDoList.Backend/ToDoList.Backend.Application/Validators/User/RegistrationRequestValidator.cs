using FluentValidation;
using ToDoList.Domain.Request;

namespace ToDoList.Application.Validators.User;

public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
{
    public RegistrationRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotNull().WithMessage("FirstName is null")
            .NotEmpty().WithMessage("FirstName is required")
            .MaximumLength(1000).WithMessage("No more than 500 characters")
            .Must(firstName => !firstName.Contains(' ')).WithMessage("FirstName must not contain spaces.");

        RuleFor(x => x.LastName)
            .NotNull().WithMessage("LastName is null")
            .NotEmpty().WithMessage("LastName is required")
            .MaximumLength(1000).WithMessage("No more than 500 characters")
            .Must(lastName => !lastName.Contains(' ')).WithMessage("LastName must not contain spaces.");

        RuleFor(x => x.Email)
            .NotNull().WithMessage("Email is null")
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("You entered an incorrect email format")
            .MaximumLength(1000).WithMessage("No more than 500 characters")
            .Must(email => !email.Contains(' ')).WithMessage("Email must not contain spaces.");

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("The password must not be empty.")
            .MinimumLength(6).WithMessage("The password must contain at least 6 characters.")
            .Matches("[0-9]").WithMessage("The password must contain at least one number.")
            .Matches("[A-Z]").WithMessage("The password must contain at least one capital letter.")
            .Matches("[a-z]").WithMessage("The password must contain at least one lowercase letter.")
            .Must(password => !password.Contains(' ')).WithMessage("The password must not contain spaces.")
            .Matches("[^a-zA-Z0-9]").WithMessage("The password must contain at least one special character.");

    }
}