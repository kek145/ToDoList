using System.Threading.Tasks;
using ToDoList.Services.Interfaces;

namespace ToDoList.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        public Task<bool> AuthenticateAsync(string email, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GenerateTokenAsync(string username)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ValidateTokenAsync(string token)
        {
            throw new System.NotImplementedException();
        }
    }
}
