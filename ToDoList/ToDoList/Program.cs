using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();

new Thread(
    () =>
    {
        app.Run(
            async(context) =>
            {
                await context.Response.SendFileAsync();
            });
    }
).Start();

app.Run();
