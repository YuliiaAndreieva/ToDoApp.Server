namespace ToDoApp.BLL.DTO;

public abstract class BaseUserTaskDto
{
    public string Name { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }

    public string? Description { get; set; }
    
    public string Status { get; set; }
}