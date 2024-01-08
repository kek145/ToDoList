using AutoMapper;
using ToDoList.Domain.DbSet;
using ToDoList.Domain.Dto;

namespace ToDoList.Application.MappingProfiles;

public class NoteMappingProfile : Profile
{
    public NoteMappingProfile()
    {
        CreateMap<Note, NoteDto>();
        CreateMap<NoteDto, Note>();
    }
}