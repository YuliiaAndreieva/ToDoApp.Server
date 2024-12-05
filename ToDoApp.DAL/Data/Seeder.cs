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
                Name = "CrossPlatformlab1",
                Description = "i need to do 13 labs",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
            },
            new UserTask()
            {
                Name = "The lesson", 
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
                Description = "lessons with Oleksandr, Gleb, Victoria.",
            },
            new UserTask()
            {
                Name = "OS Presentation", 
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(3),
                Description = "some drafts ive",
            },
            new UserTask()
            {
                Name = "Front end lab1", 
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(0),
                Description = "ive done 1-2 labs",
            },
            new UserTask()
            {
                Name = "IELTS", 
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
            },
        };
        
        _dbContext.UserTasks.AddRange(tasks);
        await _dbContext.SaveChangesAsync();
    }
}