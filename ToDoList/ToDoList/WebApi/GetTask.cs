using System.Linq;
using ToDoList.Models;
using ToDoList.Models.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ToDoList.WebApi
{
    public class GetTaskClass
    {
        public async Task GetTask(int id, HttpResponse response)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                ToDoDataBase? toDo = db?.ToDoList?.FirstOrDefault((task) => task.Id == id);

                if(toDo != null)
                {
                    await response.WriteAsJsonAsync(toDo);
                }
                else
                {
                    response.StatusCode = 404;
                    await response.WriteAsJsonAsync(new { message = "Task is not found!" });
                }
            }
        }
    }
}
