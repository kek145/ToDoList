using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ToDoList.Models;
using ToDoList.Models.Data;

namespace ToDoList.WebApi
{
    public class ChangeTaskClass
    {
        public async Task ChangeTask(HttpResponse response, HttpRequest request)
        {
            try
            {
                ToDoDataBase? toDoDb = await request.ReadFromJsonAsync<ToDoDataBase>();
                if(toDoDb != null)
                {
                    using(ApplicationContext db = new ApplicationContext())
                    {
                        var task = db?.ToDoList?.FirstOrDefault((t) => t.Id == toDoDb.Id);

                        if(task != null)
                        {
                            task.TaskName = toDoDb.TaskName;
                            task.Description = toDoDb.Description;
                            await response.WriteAsJsonAsync(task);
                        }
                    }
                }
                else
                {
                    response.StatusCode = 404;
                    await response.WriteAsJsonAsync(new { message = "Incorect data!" });
                }
            }
            catch(Exception)
            {
                response.StatusCode = 400;
                await response.WriteAsJsonAsync(new { message = "Incorect data!" });
            }
        }
    }
}
