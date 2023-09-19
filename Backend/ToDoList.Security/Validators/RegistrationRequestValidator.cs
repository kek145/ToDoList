namespace ToDoList.Security.Validators;

public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
{
    public RegistrationRequestValidator()
    {
        RuleFor(req => req.FirstName)
            .NotEmpty().WithMessage("FirstName is not empty")
            .NotNull().WithMessage("FirstName is not null")
            .Must(fName => !fName.Contains(' ')).WithMessage("FirstName must not contain spaces.");
        
        RuleFor(req => req.LastName)
            .NotEmpty().WithMessage("LastName is not empty")
            .NotNull().WithMessage("LastName is not null")
            .Must(fName => !fName.Contains(' ')).WithMessage("LastName must not contain spaces.");
        
        RuleFor(req => req.Email)
            .NotEmpty().WithMessage("Email is not empty")
            .NotNull().WithMessage("Email is not null")
            .Must(fName => !fName.Contains(' ')).WithMessage("Email must not contain spaces.");
        
        RuleFor(req => req.Password)
            .NotEmpty().WithMessage("Password is not empty")
            .NotNull().WithMessage("Password is not null")
            .Must(fName => !fName.Contains(' ')).WithMessage("Password must not contain spaces.");
        
        RuleFor(req => req.ConfirmPassword)
            .NotEmpty().WithMessage("ConfirmPassword is not empty")
            .NotNull().WithMessage("ConfirmPassword is not null")
            .Must(fName => !fName.Contains(' ')).WithMessage("ConfirmPassword must not contain spaces.");
    }
}