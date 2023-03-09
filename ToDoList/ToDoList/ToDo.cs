using ToDoList.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;
using System.IO;

namespace ToDoList
{
    public class ToDo
    {
        private List<ToDoDataBase> users = new List<ToDoDataBase>
        {
                new() {Id = 1, TaskName = "Task", Description = "qwerty"},
                new() {Id = 2, TaskName = "Task", Description = "qwerty" },
                new() {Id = 3, TaskName = "Task", Description = "qwerty"}
        };
        public async Task MainToDo(HttpContext context)
        {
            var response = context.Response;
            var request = context.Request;
            var path = request.Path;

            string? expressionForNumber = "^/api/users/([0-9]+)$";

            if (path == "/api/ToDo" && request.Method == "GET")
            {
                await GetAllTask(response);
            }
            else if (path == "/api/ToDo" && request.Method == "GET")
            {

            }
            else if (path == "/api/ToDo" && request.Method == "POST")
            {

            }
            else if (path == "/api/ToDo" && request.Method == "PUT")
            {

            }
            else if (path == "/api/ToDo" && request.Method == "DELETE")
            {

            }
            else
            {
                response.ContentType = "text/html; charset=utf-8";
                await response.SendFileAsync("wwwroot/html/index.html");
            }
        }

        private async Task GetAllTask(HttpResponse response)
        {
            await response.WriteAsJsonAsync(users);
        }
    }
}
