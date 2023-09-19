namespace ToDoList.Domain.Entities.DbSet;

public class RefreshTokenEntity
{
    public int Id { get; set; }
    public string RefreshToken { get; set; } = null!;
    public DateTime ExpiresDate { get; set; }
    public int UserId { get; set; }
    public UserEntity User { get; set; } = null!;
}