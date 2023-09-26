namespace ToDoList.DAL.Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IUserRepository UserRepository { get; }
    public ITokenRepository TokenRepository { get; }
    public ITaskRepository TaskRepository { get; }

    public UnitOfWork(
        ApplicationDbContext context,
        IUserRepository userRepository,
        ITokenRepository tokenRepository,
        ITaskRepository taskRepository)
    {
        _context = context;
        UserRepository = userRepository;
        TokenRepository = tokenRepository;
        TaskRepository = taskRepository;
    }

    public async Task<bool> CommitAsync()
    {
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }
}