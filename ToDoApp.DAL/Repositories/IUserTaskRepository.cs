using ToDoApp.DAL.Entities;

namespace ToDoApp.DAL.Repositories;

public interface IUserTaskRepository
{
    Task<IEnumerable<UserTask>> GetAll();
    
    Task<UserTask?> GetById(int id);
    
    Task Create(UserTask task);
    
    Task Update(UserTask task);
    
    Task Delete(UserTask task);
}