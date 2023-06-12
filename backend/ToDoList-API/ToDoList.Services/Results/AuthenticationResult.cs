using ToDoList.Domain.Entity;

namespace ToDoList.Services.Results
{
    public class AuthenticationResult
    {
        public bool IsAuthenticated { get; set; }
        public UserEntity? User { get; set; }
    }
}
