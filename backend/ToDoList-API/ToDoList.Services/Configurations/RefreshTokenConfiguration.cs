using System;

namespace ToDoList.Services.Configurations
{
    public class RefreshTokenConfiguration
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }
    }
}
