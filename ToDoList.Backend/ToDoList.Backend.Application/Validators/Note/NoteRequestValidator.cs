using FluentValidation;
using ToDoList.Domain.Request;

namespace ToDoList.Application.Validators.Note;

public class NoteRequestValidator : AbstractValidator<NoteRequest>
{
    public NoteRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Необхідно вказати назву")
            .NotNull().WithMessage("Необхідно вказати назву")
            .MaximumLength(1000).WithMessage("Не більше 1000 символів");
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Потрібен опис")
            .NotNull().WithMessage("Потрібен опис")
            .MaximumLength(2000).WithMessage("Не більше 2000 символів");
        
        RuleFor(x => x.Priority)
            .NotEmpty().WithMessage("Потрібен пріоритет")
            .NotNull().WithMessage("Потрібен пріоритет")
            .Must(x => !x.ToString().Contains(' ')).WithMessage("Пробіли не допускаються");

        RuleFor(x => x.Deadline)
            .NotNull().WithMessage("Термін обов’язковий")
            .NotEmpty().WithMessage("Термін обов’язковий");
    }
}