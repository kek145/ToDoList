using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<NotesToDo> Notes { get; set; }
        public ApplicationContext() => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-BEHCHLL;Initial Catalog=ToDoList;Integrated Security=True; TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NotesToDo>().HasKey((key) => key.Id);
        }
    }
}
