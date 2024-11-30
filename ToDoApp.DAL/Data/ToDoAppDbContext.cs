using Microsoft.EntityFrameworkCore;
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
}