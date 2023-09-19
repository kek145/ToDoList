namespace ToDoList.Security.Validators;

public class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequest>
{
    public AuthenticationRequestValidator()
    {
        RuleFor(req => req.Email)
            .NotEmpty().WithMessage("Email is not empty")
            .NotNull().WithMessage("Email is not null")
            .Must(fName => !fName.Contains(' ')).WithMessage("Email must not contain spaces.");
        
        RuleFor(req => req.Password)
            .NotEmpty().WithMessage("Password is not empty")
            .NotNull().WithMessage("Password is not null")
            .Must(fName => !fName.Contains(' ')).WithMessage("Password must not contain spaces.");
    }
}