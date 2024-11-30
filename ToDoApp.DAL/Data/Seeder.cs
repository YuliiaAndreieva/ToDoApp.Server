using Microsoft.EntityFrameworkCore;
using ToDoApp.DAL.Entities;

namespace ToDoApp.DAL.Data;

public class Seeder
{
    private readonly ToDoAppDbContext _dbContext;

    public Seeder(
        ToDoAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedDataAsync()
    {
        await _dbContext.Database.EnsureDeletedAsync();
        await _dbContext.Database.MigrateAsync();
        
        var tasks = new List<UserTask>()
        {
            new UserTask()
            {
                Name = "Do cross-platform lab1",
                IsDone = false, 
                Description = "i need to do 13 labs, for 1-3 lab deadline is over",
                DueDate = DateTime.Now.AddDays(2),
            },
            new UserTask()
            {
                Name = "Do 5 lessons on Monday", 
                IsDone = false, 
                DueDate = DateTime.Now.AddDays(3),
                Description = "lessons with Oleksandr, Gleb, Victoria. for lesson with Gleb i need to prepare a worksheet",
            },
            new UserTask()
            {
                Name = "Make a presentation with perfect tenses", 
                IsDone = false, 
                DueDate = DateTime.Now.AddDays(4),
                Description = "some drafts ive already created in canva",
            },
            new UserTask()
            {
                Name = "Do front-end lab 3-5", 
                IsDone = false, 
                DueDate = DateTime.Now.AddDays(5),
                Description = "ive done 1-2 labs, it's deadline for 3rd",
            },
            new UserTask()
            {
                Name = "Prepare for ielts", 
                IsDone = false, 
                DueDate = DateTime.Now.AddDays(1),
            },
            new UserTask()
            {
                Name = "Prepare for mkr on operating system", 
                IsDone = false, 
                DueDate = DateTime.Now.AddDays(2),
                Description = "hurry up",
            },
            new UserTask()
            {
                Name = "Make a presentation for philosophy", 
                IsDone = false, 
                DueDate = DateTime.Now.AddDays(6),
                Description = "hurry up its up to 2 days",
            },
        };
        
        _dbContext.UserTasks.AddRange(tasks);
        await _dbContext.SaveChangesAsync();
    }
}