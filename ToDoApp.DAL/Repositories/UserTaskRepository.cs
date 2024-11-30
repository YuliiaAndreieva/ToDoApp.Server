using Microsoft.EntityFrameworkCore;
using ToDoApp.DAL.Data;
using ToDoApp.DAL.Entities;

namespace ToDoApp.DAL.Repositories;

public class UserTaskRepository : IUserTaskRepository
{
    private readonly ToDoAppDbContext _context;

    public UserTaskRepository(
        ToDoAppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<UserTask>> GetAll()
    {
        return await _context.UserTasks.ToListAsync();
    }
    
    public async Task<UserTask?> GetById(int id)
    {
        return await _context.UserTasks.FindAsync(id);
    }

    public async Task Create(UserTask task)
    {
        _context.UserTasks.Add(task);
        await _context.SaveChangesAsync();
    }

    public async Task Update(UserTask task)
    {
        _context.UserTasks.Update(task);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(UserTask task)
    {
        _context.UserTasks.Remove(task);
        await _context.SaveChangesAsync();
    }
}