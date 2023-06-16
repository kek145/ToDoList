using System.Net;
using System.Threading.Tasks;
using ToDoList.Domain.Entity;
using ToDoList.Services.Interfaces;

namespace ToDoList.Services.Implementations
{
    public class TaskService : ITaskService
    {
        public Task<HttpStatusCode> CreateTask(TaskEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<HttpStatusCode> DeleteTask(TaskEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<HttpStatusCode> UpdateTask(TaskEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
