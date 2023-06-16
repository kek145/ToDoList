using System.Net;
using System.Threading.Tasks;
using ToDoList.Domain.Entity;

namespace ToDoList.Services.Interfaces
{
    public interface ITaskService
    {
        Task<HttpStatusCode> CreateTask(TaskEntity entity);
        Task<HttpStatusCode> DeleteTask(TaskEntity entity);
        Task<HttpStatusCode> UpdateTask(TaskEntity entity);
    }
}
