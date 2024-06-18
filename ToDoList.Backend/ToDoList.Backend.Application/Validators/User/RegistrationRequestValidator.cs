using FluentValidation;
using ToDoList.Domain.Request;

namespace ToDoList.Application.Validators.User;

public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
{
    public RegistrationRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotNull().WithMessage("Необхідно вказати ім'я")
            .NotEmpty().WithMessage("Необхідно вказати ім'я")
            .MaximumLength(100).WithMessage("Не більше 100 символів")
            .Must(firstName => !firstName.Contains(' ')).WithMessage("Ім'я не повинно містити пробілів");

        RuleFor(x => x.LastName)
            .NotNull().WithMessage("Необхідно вказати прізвище")
            .NotEmpty().WithMessage("Необхідно вказати прізвище")
            .MaximumLength(100).WithMessage("Не більше 100 символів")
            .Must(lastName => !lastName.Contains(' ')).WithMessage("Прізвище не повинно містити пробілів");

        RuleFor(x => x.Email)
            .NotNull().WithMessage("Необхідно вказати адресу електронної пошти")
            .NotEmpty().WithMessage("Необхідно вказати адресу електронної пошти")
            .EmailAddress().WithMessage("Ви ввели неправильний формат електронної пошти")
            .Must(email => !email.Contains(' ')).WithMessage("Електронна адреса не повинна містити пробілів");

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Пароль не повинен бути порожнім")
            .MinimumLength(6).WithMessage("Пароль має містити не менше 6 символів")
            .Matches("[A-Z]").WithMessage("Пароль повинен містити хоча б одну велику літеру")
            .Matches("[a-z]").WithMessage("Пароль має містити принаймні одну малу літеру")
            .Must(password => !password.Contains(' ')).WithMessage("Пароль не повинен містити пробілів")
            .Matches("[^a-zA-Z0-9]").WithMessage("Пароль повинен містити хоча б один спеціальний символ.");

    }
}