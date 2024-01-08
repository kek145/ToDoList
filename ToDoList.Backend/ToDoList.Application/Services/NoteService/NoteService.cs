using MediatR;
using FluentValidation;
using System.Threading.Tasks;
using ToDoList.Domain.Result;
using ToDoList.Domain.Request;
using ToDoList.Application.Exceptions;
using ToDoList.Application.Commands.Notes.Create;

namespace ToDoList.Application.Services.NoteService;

public class NoteService : INoteService
{
    private readonly IMediator _mediator;
    private readonly IValidator<NoteRequest> _validator;

    public NoteService(IMediator mediator, IValidator<NoteRequest> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }

    public async Task<NoteResponse> CreateNoteAsync(NoteRequest request)
    {
        var validator = await _validator.ValidateAsync(request);

        if (!validator.IsValid)
            throw new BadRequestException($"Validation error: {validator}");

        var command = new CreateNoteCommand(request, 1);

        var result = await _mediator.Send(command);

        return result;
    }
}