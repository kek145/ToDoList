using System;
using AutoMapper;
using System.Linq;
using System.Threading;
using ToDoList.Domain.Dto;

using ToDoList.Domain.DbSet;
using ToDoList.Domain.Result;
using System.Threading.Tasks;
using ToDoList.Domain.Helpers;
using ToDoList.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using ToDoList.Infrastructure.DataStore;

namespace ToDoList.Infrastructure.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;

    public NoteRepository(IMapper mapper, ApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public IQueryable<NoteDto> GetAll()
    {
        return _context.Notes
            .AsQueryable().ProjectTo<NoteDto>(_mapper.ConfigurationProvider);
    }

    public async Task DeleteNoteAsync(int noteId, CancellationToken cancellationToken = default)
    {
        var note = await GetNoteByIdAsync(noteId, cancellationToken);

        var noteDto = _mapper.Map<Note>(note);

        _context.Notes.Remove(noteDto);
    }

    public async Task<NoteDto?> GetNoteByIdAsync(int noteId, CancellationToken cancellationToken = default)
    {
        var note = await _context.Notes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == noteId, cancellationToken);
        
        var result = _mapper.Map<NoteDto>(note);

        return result;
    }

    public void UpdateNote(NoteDto noteDto)
    {
        var note = _mapper.Map<Note>(noteDto);
        _context.Notes.Update(note);
    }

    public async Task<NoteDto> AddNoteAsync(NoteDto noteDto, CancellationToken cancellationToken = default)
    {
        var note = _mapper.Map<Note>(noteDto);

        var task = await _context.Notes.AddAsync(note, cancellationToken);

        var result = _mapper.Map<NoteDto>(task.Entity);

        return result;
    }

    public async Task<PagedResult<NoteDto>> GetAllNotesAsync(QueryParameters queryParameters, int userId, CancellationToken cancellationToken = default)
    {
        if (queryParameters.PageNumber < 1)
            queryParameters.PageNumber = 1;
        
        var query = _context.Notes
            .AsNoTracking()
            .Where(x => x.Status == false &&
                        x.Deadline > DateTime.UtcNow &&
                        x.UserId == userId)
            .OrderBy(x => x.CreatedAt);

        return await PaginationAsync(queryParameters, query.Select(x => new NoteDto
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            Priority = x.Priority,
            Status = x.Status,
            Deadline = x.Deadline,
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt,
            UserId = x.UserId
        }), cancellationToken);
    }

    public async Task<PagedResult<NoteDto>> GetAllFailedNotesAsync(QueryParameters queryParameters, int userId, CancellationToken cancellationToken = default)
    {
        if (queryParameters.PageNumber < 1)
            queryParameters.PageNumber = 1;
        
        var query = _context.Notes
            .AsNoTracking()
            .Where(x => x.Status == false &&
                        x.Deadline < DateTime.UtcNow &&
                        x.UserId == userId)
            .OrderBy(x => x.CreatedAt);

        return await PaginationAsync(queryParameters, query.Select(x => new NoteDto
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            Priority = x.Priority,
            Status = x.Status,
            Deadline = x.Deadline,
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt,
            UserId = x.UserId
        }), cancellationToken);
    }

    public async Task<PagedResult<NoteDto>> GetAllCompletedNotesAsync(QueryParameters queryParameters, int userId, CancellationToken cancellationToken = default)
    {
        if (queryParameters.PageNumber < 1)
            queryParameters.PageNumber = 1;
        
        var query = _context.Notes
            .AsNoTracking()
            .Where(x => x.Status == true &&
                        x.UserId == userId)
            .ProjectTo<NoteDto>(_mapper.ConfigurationProvider)
            .OrderBy(x => x.CreatedAt);

        return await PaginationAsync(queryParameters, query, cancellationToken);
    }

    public async Task<PagedResult<NoteDto>> GetAllByPriorityNotesAsync(QueryParameters queryParameters, string priority, int userId, CancellationToken cancellationToken = default)
    {
        if (queryParameters.PageNumber < 1)
            queryParameters.PageNumber = 1;
        
        
        var query = _context.Notes
            .AsNoTracking()
            .Where(x => x.Status == false &&
                        x.Deadline > DateTime.UtcNow &&
                        x.UserId == userId &&
                        x.Priority == priority)
            .ProjectTo<NoteDto>(_mapper.ConfigurationProvider)
            .OrderBy(x => x.CreatedAt);
        
        return await PaginationAsync(queryParameters, query, cancellationToken);
    }

    private static async Task<PagedResult<NoteDto>> PaginationAsync(QueryParameters queryParameters, IQueryable<NoteDto> query, CancellationToken cancellationToken = default)
    {
        var totalSize = await query.CountAsync(cancellationToken);
        
        var startIndex = (queryParameters.PageNumber - 1) * queryParameters.PageSize;

        var items = await query.AsQueryable()
            .Skip(startIndex)
            .Take(queryParameters.PageSize)
            .ToListAsync(cancellationToken);
        
        var totalPages = (int)Math.Ceiling((double)totalSize / queryParameters.PageSize);
        
        return new PagedResult<NoteDto>
        {
            Items = items,
            PageNumber = queryParameters.PageNumber,
            RecordNumber = queryParameters.PageSize,
            TotalCount = totalSize,
            TotalPages = totalPages
        };
    }
}