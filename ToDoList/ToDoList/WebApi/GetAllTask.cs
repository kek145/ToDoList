using System.Linq;
using ToDoList.Models.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ToDoList.WebApi
{
    public class GetAllTask
    {
        public async Task GetAllTasks(HttpResponse response)
        {
            using(ApplicationContext? db = new ApplicationContext())
            {
                var toDo = db?.ToDoList?.ToList();
                foreach(var user in toDo!)
                {
                    await response.WriteAsJsonAsync(user);
                }
            }
        }
    }
}
