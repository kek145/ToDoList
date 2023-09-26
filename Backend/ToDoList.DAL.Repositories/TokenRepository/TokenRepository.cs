namespace ToDoList.DAL.Repositories.TokenRepository;

public class TokenRepository: GenericRepository<RefreshTokenEntity>, ITokenRepository
{
    public TokenRepository(ApplicationDbContext context) : base(context) { }
}