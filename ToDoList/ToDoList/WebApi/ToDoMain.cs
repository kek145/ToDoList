using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace ToDoList.WebApi
{
    public class ToDoMain
    {
        private GetAllTask _getAllTask = new GetAllTask();
        private GetTaskClass _getTaskClass = new GetTaskClass();
        private DeleteTaskClass _deleteTaskClass = new DeleteTaskClass();
        private CreateTaskClass _createTaskClass = new CreateTaskClass();

        public async Task MainToDo(HttpContext context)
        {
            var response = context.Response;
            var request = context.Request;
            var path = request.Path;

            string? expressionForNumber = "^/api/users/([0-9]+)$";

            if (path == "/api/ToDo" && request.Method == "GET")
            {
                await _getAllTask.GetAllTasks(response);
            }
            else if (Regex.IsMatch(path, expressionForNumber) && request.Method == "GET")
            {
                string? idString = path.Value?.Split("/")[3];

                int id = Convert.ToInt32(idString);
                
                await _getTaskClass.GetTask(id, response);
            }
            else if (path == "/api/ToDo" && request.Method == "POST")
            {
                await _createTaskClass.CreateTask(response, request);
            }
            else if (path == "/api/ToDo" && request.Method == "PUT")
            {

            }
            else if (Regex.IsMatch(path, expressionForNumber) && request.Method == "DELETE")
            {
                string? idString = path.Value?.Split("/")[3];

                int id = Convert.ToInt32(idString);

                await _deleteTaskClass.DeleteTask(id, response);
            }
            else
            {
                response.ContentType = "text/html; charset=utf-8";
                await response.SendFileAsync("wwwroot/html/index.html");
            }
        }
    }
}
