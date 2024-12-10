using System.Xml.Serialization;

namespace ToDoApp.WebApi.Helpers;

public class Link
{
    [XmlAttribute("Rel")]
    public string Rel { get; set; } = null!;
    
    public string Href { get; set; } = null!;

    public string Method { get; set; } = null!;
    
    public Link() { }
}