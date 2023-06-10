using System.Threading.Tasks;

namespace ToDoList.Services.Interfaces
{
    public interface IRegistrationService
    {
        Task RegisterAsync(string username, string email, string password);
    }
}
