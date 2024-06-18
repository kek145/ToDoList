using FluentValidation;
using ToDoList.Domain.Request;

namespace ToDoList.Application.Validators.User;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotNull().WithMessage("Необхідно вказати адресу електронної пошти")
            .NotEmpty().WithMessage("Необхідно вказати адресу електронної пошти")
            .EmailAddress().WithMessage("Ви ввели неправильний формат електронної пошти")
            .Must(email => !email.Contains(' ')).WithMessage("Електронна адреса не повинна містити пробілів");

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Пароль не повинен бути порожнім")
            .Must(password => !password.Contains(' ')).WithMessage("Пароль не повинен містити пробілів.");
    }
}