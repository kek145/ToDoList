using System.Threading.Tasks;

namespace ToDoList.Services.Interfaces
{
    public interface IAuthorizationService
    {
        Task<bool> CheckAccessAsync(string userId, string resourceId);
    }
}
