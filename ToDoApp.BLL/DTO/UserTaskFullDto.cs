namespace ToDoApp.BLL.DTO;

public class UserTaskFullDto
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public string? Description { get; set; } 
    
    public DateTime DueDate { get; set; }
    
    public string Status { get; set; }
}