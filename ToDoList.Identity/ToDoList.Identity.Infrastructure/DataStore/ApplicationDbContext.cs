using Microsoft.EntityFrameworkCore;

namespace ToDoList.Identity.Infrastructure.DataStore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    
}