using FluentValidation;
using ToDoList.Domain.Contracts.Request;

namespace ToDoList.Security.Validators;

public class TaskRequestValidator : AbstractValidator<TaskRequestDto>
{
    public TaskRequestValidator()
    {
        RuleFor(req => req.Title)
            .NotEmpty().WithMessage("Title is not empty")
            .NotNull().WithMessage("Title is not null");
        
        RuleFor(req => req.Description)
            .NotEmpty().WithMessage("Description is not empty")
            .NotNull().WithMessage("Description is not null");
        
        RuleFor(req => req.Priority)
            .NotEmpty().WithMessage("Priority is not empty")
            .NotNull().WithMessage("Priority is not null");
        
        RuleFor(req => req.Deadline)
            .NotEmpty().WithMessage("DeadLine is not empty")
            .NotNull().WithMessage("DeadLine is not null");
    }
}