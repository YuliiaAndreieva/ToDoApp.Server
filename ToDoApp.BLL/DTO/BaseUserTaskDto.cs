namespace ToDoApp.BLL.DTO;

public abstract class BaseUserTaskDto
{
    public string Name { get; set; }
    
    public DateTime DueDate { get; set; }
    
    public string? Description { get; set; }
    
    public bool IsDone { get; set; }
}