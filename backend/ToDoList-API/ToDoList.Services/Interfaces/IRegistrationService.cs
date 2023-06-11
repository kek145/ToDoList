using System.Net;
using System.Threading.Tasks;

namespace ToDoList.Services.Interfaces
{
    public interface IRegistrationService
    {
        Task<bool> IsEmailExistsAsync(string email);
        Task<HttpStatusCode> RegisterAsync(string username, string email, string password, string confirmpassword);
    }
}
