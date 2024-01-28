using System;
using AutoMapper;
using System.Linq;
using System.Threading;
using ToDoList.Domain.Dto;
using ToDoList.Domain.DbSet;
using ToDoList.Domain.Result;
using System.Threading.Tasks;
using ToDoList.Domain.Helpers;
using ToDoList.Domain.Repositories;
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

    public async Task<long> CompleteNoteAsync(long noteId, CancellationToken cancellationToken)
    {
        return await _context.Notes
            .Where(x => x.Id == noteId)
            .ExecuteUpdateAsync(x => x.SetProperty(c => c.Status, true), cancellationToken);
    }

    public async Task<long> DeleteNoteAsync(long noteId, CancellationToken cancellationToken = default)
    {
        return await _context.Notes
            .Where(x => x.Id == noteId)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<NoteDto?> GetNoteByIdAsync(long noteId, CancellationToken cancellationToken = default)
    {
        var note = await _context.Notes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == noteId, cancellationToken);
        
        var result = _mapper.Map<NoteDto>(note);

        return result;
    }

    public async Task<long> UpdateNoteAsync(long noteId, NoteDto noteDto, CancellationToken cancellationToken)
    {
        return await _context.Notes
            .Where(x => x.Id == noteId)
            .ExecuteUpdateAsync(x => x
                    .SetProperty(c => c.Title, noteDto.Title)
                    .SetProperty(c => c.Description, noteDto.Description)
                    .SetProperty(c => c.Priority, noteDto.Priority)
                    .SetProperty(c => c.Deadline, noteDto.Deadline)
                    .SetProperty(c => c.UpdatedAt, DateTime.UtcNow), 
            cancellationToken);
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
            .ProjectTo<NoteDto>(_mapper.ConfigurationProvider)
            .OrderBy(x => x.CreatedAt);

        return await PaginationAsync(queryParameters, query, cancellationToken);
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
            .ProjectTo<NoteDto>(_mapper.ConfigurationProvider)
            .OrderBy(x => x.CreatedAt);

        return await PaginationAsync(queryParameters, query, cancellationToken);
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