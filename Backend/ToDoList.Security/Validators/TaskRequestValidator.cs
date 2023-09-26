namespace ToDoList.Security.Validators;

public class TaskRequestValidator : AbstractValidator<TaskRequest>
{
    public TaskRequestValidator()
    {
        RuleFor(req => req.Title)
            .NotEmpty().WithMessage("Title is not empty")
            .NotNull().WithMessage("Title is not null");
        
        RuleFor(req => req.Description)
            .NotEmpty().WithMessage("Description is not empty")
            .NotNull().WithMessage("Description is not null");
        
        RuleFor(req => req.DeadLine)
            .NotEmpty().WithMessage("DeadLine is not empty")
            .NotNull().WithMessage("DeadLine is not null");
    }
}