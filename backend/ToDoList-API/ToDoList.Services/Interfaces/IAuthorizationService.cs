using System.Threading.Tasks;

namespace ToDoList.Services.Interfaces
{
    public interface IAuthorizationService
    {
        Task<bool> CheckAccessAsync(int userId, string resourceId);
    }
}
