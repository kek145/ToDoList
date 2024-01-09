using MediatR;
using FluentValidation;
using ToDoList.Domain.Enum;
using System.Threading.Tasks;
using ToDoList.Domain.Result;
using ToDoList.Domain.Helpers;
using ToDoList.Domain.Request;
using ToDoList.Application.Exceptions;
using ToDoList.Application.Commands.Notes.Patch;
using ToDoList.Application.Queries.Notes.GetAll;
using ToDoList.Application.Commands.Notes.Delete;
using ToDoList.Application.Commands.Notes.Create;
using ToDoList.Application.Commands.Notes.Update;
using ToDoList.Application.Queries.Notes.GetAllByPriority;
using ToDoList.Application.Queries.Notes.GetById;
using ToDoList.Application.Queries.Notes.GetAllFailed;
using ToDoList.Application.Queries.Notes.GetAllCompleted;

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

    public async Task<NoteResponse> GetNoteByIdAsync(int noteId)
    {
        if (noteId <= 0)
            throw new BadRequestException("Id cannot be less than or equal to zero!");

        var query = new GetNoteByIdQuery(noteId, 1);

        var result = await _mediator.Send(query);

        return result;
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

    public async Task<PagedResult<NoteResponse>> GetAllNotesAsync(QueryParameters queryParameters)
    {
        var query = new GetAllNotesQuery(queryParameters, 1);

        var result = await _mediator.Send(query);

        return result;
    }

    public async Task<PagedResult<NoteResponse>> GetAllFailedNotesAsync(QueryParameters queryParameters)
    {
        var query = new GetAllFailedNotesQuery(queryParameters, 1);

        var result = await _mediator.Send(query);

        return result;
    }

    public async Task<PagedResult<NoteResponse>> GetAllCompletedNotesAsync(QueryParameters queryParameters)
    {
        var query = new GetAllCompletedNotesQuery(queryParameters, 1);

        var result = await _mediator.Send(query);

        return result;
    }

    public async Task<PagedResult<NoteResponse>> GetAllByPriorityNotesAsync(QueryParameters queryParameters, Priority priority)
    {
        var query = priority switch
        {
            Priority.Easy => new GetAllByPriorityNotesQuery(queryParameters, PrioritiesHelper.Easy, 1),
            Priority.Medium => new GetAllByPriorityNotesQuery(queryParameters, PrioritiesHelper.Medium, 1),
            Priority.Hard => new GetAllByPriorityNotesQuery(queryParameters, PrioritiesHelper.Hard, 1),
            _ => throw new BadRequestException("There is no such priority!")
        };

        var result = await _mediator.Send(query);

        return result;
    }
}