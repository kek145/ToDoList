using System;
using MediatR;
using System.Net;
using AutoMapper;
using FluentValidation;
using ToDoList.Domain.Enum;
using System.Threading.Tasks;
using ToDoList.Domain.Result;
using ToDoList.Domain.Helpers;
using ToDoList.Domain.Request;
using Microsoft.AspNetCore.Http;
using ToDoList.Domain.Abstractions;
using ToDoList.Application.Helpers;
using ToDoList.Application.Exceptions;
using ToDoList.Domain.Implementations;
using ToDoList.Application.Commands.Notes.Patch;
using ToDoList.Application.Queries.Notes.GetAll;
using ToDoList.Application.Commands.Notes.Delete;
using ToDoList.Application.Commands.Notes.Create;
using ToDoList.Application.Commands.Notes.Update;
using ToDoList.Application.Queries.Notes.GetById;
using ToDoList.Application.Queries.Notes.GetAllFailed;
using ToDoList.Application.Queries.Notes.GetAllCompleted;
using ToDoList.Application.Queries.Notes.GetAllByPriority;

namespace ToDoList.Application.Services.NoteService;

public class NoteService(IMapper mapper, IMediator mediator, IValidator<NoteRequest> validator, IHttpContextAccessor contextAccessor) : INoteService
{
    private readonly IMapper _mapper = mapper;
    private readonly IMediator _mediator = mediator;
    private readonly IValidator<NoteRequest> _validator = validator;
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

    public async Task<HttpStatusCode> DeleteNoteAsync(long noteId)
    {
        var userId = Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirst("UserId")?.Value);
        
        if (noteId <= 0)
            throw new BadRequestException("Id cannot be less than or equal to zero!");

        var command = new DeleteNoteCommand(userId, noteId);

        var result = await _mediator.Send(command);

        return result > 0 ? HttpStatusCode.NoContent : HttpStatusCode.NotFound;
    }

    public async Task<HttpStatusCode> CompleteNoteAsync(long noteId)
    {
        var userId = Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirst("UserId")?.Value);
        
        if (noteId <= 0)
            throw new BadRequestException("Id cannot be less than or equal to zero!");

        var command = new CompleteNoteCommand(noteId, userId);

        var result = await _mediator.Send(command);
        
        return result > 0 ? HttpStatusCode.NoContent : HttpStatusCode.NotFound;
    }

    public async Task<IBaseResponse<NoteResponse>> GetNoteByIdAsync(long noteId)
    {
        var userId = Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirst("UserId")?.Value);
        
        if (noteId <= 0)
            throw new BadRequestException("Id cannot be less than or equal to zero!");

        var query = new GetNoteByIdQuery(noteId, userId);

        var note = await _mediator.Send(query);

        var data = _mapper.Map<NoteResponse>(note);

        return new BaseResponse<NoteResponse>
        {
            StatusCode = HttpStatusCode.OK,
            Message = MessageResponseHelper.Success,
            Data = data
        };
    }

    public async Task<HttpStatusCode> UpdateNoteAsync(NoteRequest request, long noteId)
    {
        var userId = Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirst("UserId")?.Value);
        
        if (noteId <= 0)
            throw new BadRequestException("Id cannot be less than or equal to zero!");

        var validator = await _validator.ValidateAsync(request);

        if (!validator.IsValid)
            throw new BadRequestException($"Validation error: {validator}");

        var command = new UpdateNoteCommand(userId, noteId, request);

        var result = await _mediator.Send(command);
        
        return result > 0 ? HttpStatusCode.NoContent : HttpStatusCode.NotFound;
    }

    public async Task<IBaseResponse<NoteResponse>> CreateNoteAsync(NoteRequest request)
    {
        var userId = Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirst("UserId")?.Value);
        
        var validator = await _validator.ValidateAsync(request);

        if (!validator.IsValid)
            throw new BadRequestException($"Validation error: {validator}");

        var command = new CreateNoteCommand(request, userId);

        var note = await _mediator.Send(command);

        var data = _mapper.Map<NoteResponse>(note);

        return new BaseResponse<NoteResponse>
        {
            StatusCode = HttpStatusCode.Created,
            Message = "Note created successfully",
            Data = data
        };
    }

    public async Task<IBaseResponse<PagedResult<NoteResponse>>> GetAllNotesAsync(QueryParameters queryParameters)
    {
        var userId = Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirst("UserId")?.Value);
        
        var query = new GetAllNotesQuery(queryParameters, userId);

        var notes = await _mediator.Send(query);

        var data = _mapper.Map<PagedResult<NoteResponse>>(notes);

        return new BaseResponse<PagedResult<NoteResponse>>
        {
            StatusCode = HttpStatusCode.OK,
            Message = MessageResponseHelper.Success,
            Data = data
        };
    }

    public async Task<IBaseResponse<PagedResult<NoteResponse>>> GetAllFailedNotesAsync(QueryParameters queryParameters)
    {
        var userId = Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirst("UserId")?.Value);
        
        var query = new GetAllFailedNotesQuery(queryParameters, userId);

        var notes = await _mediator.Send(query);

        var data = _mapper.Map<PagedResult<NoteResponse>>(notes);

        return new BaseResponse<PagedResult<NoteResponse>>
        {
            StatusCode = HttpStatusCode.OK,
            Message = MessageResponseHelper.Success,
            Data = data
        };
    }

    public async Task<IBaseResponse<PagedResult<NoteResponse>>> GetAllCompletedNotesAsync(QueryParameters queryParameters)
    {
        var userId = Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirst("UserId")?.Value);
        
        var query = new GetAllCompletedNotesQuery(queryParameters, userId);

        var notes = await _mediator.Send(query);

        var data = _mapper.Map<PagedResult<NoteResponse>>(notes);

        return new BaseResponse<PagedResult<NoteResponse>>
        {
            StatusCode = HttpStatusCode.OK,
            Message = MessageResponseHelper.Success,
            Data = data
        };
    }

    public async Task<IBaseResponse<PagedResult<NoteResponse>>> GetAllByPriorityNotesAsync(QueryParameters queryParameters, Priority priority)
    {
        var userId = Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirst("UserId")?.Value);
        
        var query = priority switch
        {
            Priority.Easy => new GetAllByPriorityNotesQuery(queryParameters, PrioritiesHelper.Easy, userId),
            Priority.Medium => new GetAllByPriorityNotesQuery(queryParameters, PrioritiesHelper.Medium, userId),
            Priority.Hard => new GetAllByPriorityNotesQuery(queryParameters, PrioritiesHelper.Hard, userId),
            _ => throw new BadRequestException("There is no such priority!")
        };

        var notes = await _mediator.Send(query);

        var data = _mapper.Map<PagedResult<NoteResponse>>(notes);

        return new BaseResponse<PagedResult<NoteResponse>>
        {
            StatusCode = HttpStatusCode.OK,
            Message = MessageResponseHelper.Success,
            Data = data
        };
    }
}