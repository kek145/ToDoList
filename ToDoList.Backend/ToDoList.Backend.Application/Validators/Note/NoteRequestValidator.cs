using FluentValidation;
using ToDoList.Domain.Request;

namespace ToDoList.Application.Validators.Note;

public class NoteRequestValidator : AbstractValidator<NoteRequest>
{
    public NoteRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(1000).WithMessage("No more than 500 characters")
            .NotNull().WithMessage("Title is null");
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(2000).WithMessage("No more than 2000 characters")
            .NotNull().WithMessage("Description is null");
        
        RuleFor(x => x.Priority)
            .NotEmpty().WithMessage("Priority is required")
            .Must(x => !x.ToString().Contains(' ')).WithMessage("Spaces are not allowed")
            .NotNull().WithMessage("Priority is null");
        
        RuleFor(x => x.Deadline)
            .NotEmpty().WithMessage("Deadline is required")
            .NotNull().WithMessage("Deadline is null");
    }
}