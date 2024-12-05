using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDoApp.DAL.Entities;


namespace ToDoApp.DAL.Data;

public class ToDoAppDbContext : DbContext
{
    public ToDoAppDbContext(
        DbContextOptions<ToDoAppDbContext> contextOptions)
        : base(contextOptions)
    {
        
    }
    
    public DbSet<UserTask> UserTasks { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserTask>()
            .Property(t => t.Status)
            .HasConversion(new EnumToStringConverter<UserTaskStatus>());
    }
}