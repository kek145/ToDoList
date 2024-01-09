using MediatR;
using FluentValidation;
using System.Threading.Tasks;
using ToDoList.Domain.Result;
using ToDoList.Domain.Request;
using ToDoList.Application.Exceptions;
using ToDoList.Application.Commands.Notes.Create;
using ToDoList.Application.Commands.Notes.Delete;
using ToDoList.Application.Commands.Notes.Patch;
using ToDoList.Application.Commands.Notes.Update;

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

    public async Task DeleteNoteAsync(int noteId)
    {
        if (noteId <= 0)
            throw new BadRequestException("Id cannot be less than or equal to zero!");

        var command = new DeleteNoteCommand(1, noteId);

        await _mediator.Send(command);
    }

    public async Task CompleteNoteAsync(int noteId)
    {
        if (noteId <= 0)
            throw new BadRequestException("Id cannot be less than or equal to zero!");

        var command = new CompleteNoteCommand(noteId, 1);

        await _mediator.Send(command);
    }

    public async Task UpdateNoteAsync(NoteRequest request, int noteId)
    {
        if (noteId <= 0)
            throw new BadRequestException("Id cannot be less than or equal to zero!");

        var validator = await _validator.ValidateAsync(request);

        if (!validator.IsValid)
            throw new BadRequestException($"Validation error: {validator}");

        var command = new UpdateNoteCommand(1, noteId, request);

        await _mediator.Send(command);
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