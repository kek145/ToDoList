using System;
using AutoMapper;
using System.Linq;
using System.Threading;
using ToDoList.Domain.Dto;
using ToDoList.Domain.DbSet;
using System.Threading.Tasks;
using ToDoList.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using ToDoList.Infrastructure.DataStore;
namespace ToDoList.Infrastructure.Repositories;

public class UserRepository(IMapper mapper, ApplicationDbContext context) : IUserRepository
{
    private readonly IMapper _mapper = mapper;
    private readonly ApplicationDbContext _context = context;
    public async Task<int> DeleteUserAsync(int userId, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Where(x => x.Id == userId)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<UserDto?> GetUserById(int userId, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        
        var result = _mapper.Map<UserDto>(user);

        return result;
    }

    public async Task<string> GetUserFullName(int userId, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

        if (user is null)
            throw new UnauthorizedAccessException("User invalid!");

        return $"{user.FirstName} {user.LastName}";
    }

    public async Task<UserDto?> GetUserByEmail(string email, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

        var result = _mapper.Map<UserDto>(user);

        return result;
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