using System;
using MediatR;
using FluentValidation;
using ToDoList.Domain.Enum;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    private readonly IHttpContextAccessor _contextAccessor;

    public NoteService(IMediator mediator, IValidator<NoteRequest> validator, IHttpContextAccessor contextAccessor)
    {
        _mediator = mediator;
        _validator = validator;
        _contextAccessor = contextAccessor;
    }

    public async Task DeleteNoteAsync(int noteId)
    {
        var userId = Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirst("UserId")?.Value);
        
        if (noteId <= 0)
            throw new BadRequestException("Id cannot be less than or equal to zero!");

        var command = new DeleteNoteCommand(userId, noteId);

        await _mediator.Send(command);
    }

    public async Task CompleteNoteAsync(int noteId)
    {
        var userId = Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirst("UserId")?.Value);
        
        if (noteId <= 0)
            throw new BadRequestException("Id cannot be less than or equal to zero!");

        var command = new CompleteNoteCommand(noteId, userId);

        await _mediator.Send(command);
    }

    public async Task<NoteResponse> GetNoteByIdAsync(int noteId)
    {
        var userId = Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirst("UserId")?.Value);
        
        if (noteId <= 0)
            throw new BadRequestException("Id cannot be less than or equal to zero!");

        var query = new GetNoteByIdQuery(noteId, userId);

        var result = await _mediator.Send(query);

        return result;
    }

    public async Task UpdateNoteAsync(NoteRequest request, int noteId)
    {
        var userId = Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirst("UserId")?.Value);
        
        if (noteId <= 0)
            throw new BadRequestException("Id cannot be less than or equal to zero!");

        var validator = await _validator.ValidateAsync(request);

        if (!validator.IsValid)
            throw new BadRequestException($"Validation error: {validator}");

        var command = new UpdateNoteCommand(userId, noteId, request);

        await _mediator.Send(command);
    }

    public async Task<NoteResponse> CreateNoteAsync(NoteRequest request)
    {
        var userId = Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirst("UserId")?.Value);
        
        var validator = await _validator.ValidateAsync(request);

        if (!validator.IsValid)
            throw new BadRequestException($"Validation error: {validator}");

        var command = new CreateNoteCommand(request, userId);

        var result = await _mediator.Send(command);

        return result;
    }

    public async Task<PagedResult<NoteResponse>> GetAllNotesAsync(QueryParameters queryParameters)
    {
        var userId = Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirst("UserId")?.Value);
        
        var query = new GetAllNotesQuery(queryParameters, userId);

        var result = await _mediator.Send(query);

        return result;
    }

    public async Task<PagedResult<NoteResponse>> GetAllFailedNotesAsync(QueryParameters queryParameters)
    {
        var userId = Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirst("UserId")?.Value);
        
        var query = new GetAllFailedNotesQuery(queryParameters, userId);

        var result = await _mediator.Send(query);

        return result;
    }

    public async Task<PagedResult<NoteResponse>> GetAllCompletedNotesAsync(QueryParameters queryParameters)
    {
        var userId = Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirst("UserId")?.Value);
        
        var query = new GetAllCompletedNotesQuery(queryParameters, userId);

        var result = await _mediator.Send(query);

        return result;
    }

    public async Task<PagedResult<NoteResponse>> GetAllByPriorityNotesAsync(QueryParameters queryParameters, Priority priority)
    {
        var userId = Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirst("UserId")?.Value);
        
        var query = priority switch
        {
            Priority.Easy => new GetAllByPriorityNotesQuery(queryParameters, PrioritiesHelper.Easy, userId),
            Priority.Medium => new GetAllByPriorityNotesQuery(queryParameters, PrioritiesHelper.Medium, userId),
            Priority.Hard => new GetAllByPriorityNotesQuery(queryParameters, PrioritiesHelper.Hard, userId),
            _ => throw new BadRequestException("There is no such priority!")
        };

        var result = await _mediator.Send(query);

        return result;
    }
}