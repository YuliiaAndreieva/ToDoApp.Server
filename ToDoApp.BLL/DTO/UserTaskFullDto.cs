

using System.Xml.Serialization;

namespace ToDoApp.BLL.DTO;

[XmlRoot("UserTask")]
public class UserTaskFullDto
{
    [XmlElement("TaskId")] 
    public int Id { get; set; }
    
    [XmlElement("Name")] 
    public string Name { get; set; }
    
    [XmlElement("Description")] 
    public string? Description { get; set; } 
    
    [XmlElement("StartDate")] 
    public DateTime StartDate { get; set; }
    
    [XmlElement("EndDate")] 
    public DateTime EndDate { get; set; }
    
    [XmlElement("Status")] 
    public string Status { get; set; }
}