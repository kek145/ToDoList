using System.Linq;
using AutoMapper;
using System.Threading;
using ToDoList.Domain.Dto;
using ToDoList.Domain.DbSet;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Repositories;
using ToDoList.Infrastructure.DataStore;

namespace ToDoList.Infrastructure.Repositories;

public class UserRepository(IMapper mapper, ApplicationDbContext context) : IUserRepository
{
    private readonly IMapper _mapper = mapper;
    private readonly ApplicationDbContext _context = context;

    public IQueryable<UserDto> GetAll()
    {
        return _context.Users
            .AsQueryable().ProjectTo<UserDto>(_mapper.ConfigurationProvider);
    }
    public async Task<UserDto> AddUserAsync(UserDto userDto, CancellationToken cancellationToken = default)
    {
        var user = _mapper.Map<User>(userDto);

        var newUser = await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<UserDto>(newUser.Entity);

        return result;
    }
}