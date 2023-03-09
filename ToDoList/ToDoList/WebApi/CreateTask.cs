using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ToDoList.Models;
using ToDoList.Models.Data;

namespace ToDoList.WebApi
{
    public class CreateTaskClass
    {
        public async Task CreateTask(HttpResponse response, HttpRequest request)
        {
            try
            {
                var json = request?.ReadFromJsonAsync<ToDoDataBase>();
                if(json != null)
                {
                    var form = request?.Form;
                    var task = form?["taskName"];
                    var description = form?["description"];
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        ToDoDataBase toDo = new ToDoDataBase { TaskName = task, Description = description };
                        db.Add(toDo);
                        db.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("Incorect data!");
                }
            }
            catch (Exception)
            {
                response.StatusCode = 400;
                await response.WriteAsJsonAsync(new { message = "Incorect data!" });
            }
        }
    }
}
