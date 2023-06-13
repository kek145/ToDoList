using System;

namespace ToDoList.Domain.Entity
{
    public class RefreshTokenEntity
    {
        public int TokenId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public int UserID { get; set; }
        public UserEntity? User { get; set; }
    }
}
