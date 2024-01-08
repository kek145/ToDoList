using AutoMapper;
using System.Linq;
using System.Threading;
using ToDoList.Domain.Dto;
using ToDoList.Domain.DbSet;
using System.Threading.Tasks;
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

    public async Task<NoteDto?> GetNoteByIdAsync(int taskId, CancellationToken cancellationToken = default)
    {
        var note = await _context.Notes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == taskId, cancellationToken);
        
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
}