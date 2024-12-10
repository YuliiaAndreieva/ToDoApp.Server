using System.Xml.Serialization;

namespace ToDoApp.WebApi.Helpers;

[XmlRoot("Resource")]
public class Resource<T>()
{
    [XmlElement("Data")]
    public T Data { get; set; }

    [XmlArray("Links")]
    [XmlArrayItem("Link")]
    public List<Link> Links { get; set; } = new();

    public Resource(T data) : this()
    {
        Data = data;
    }
}
