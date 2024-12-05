using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.DAL.Entities;

public class UserTask
{
    public int Id { get; set; }
    
    public string Name { get; set; } = default!;

    public string? Description { get; set; } 
    
    [Column(TypeName = "datetime2(0)")]
    public DateTime DueDate { get; set; }

    public UserTaskStatus Status { get; set; } = UserTaskStatus.Planned;
}