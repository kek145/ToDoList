using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ToDoList.Models;

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
