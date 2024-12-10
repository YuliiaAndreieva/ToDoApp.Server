namespace ToDoApp.WebApi.Helpers;

public static class LinkBuilderHelper
{
    public static List<Link> CreateLinks(int id)
    {
        return new List<Link>
        {
            new() { Rel = "self", Href = $"/api/task/{id}", Method = "GET" },
            new() { Rel = "delete", Href = $"/api/task/{id}", Method = "DELETE" },
            new() { Rel = "update", Href = "/api/task", Method = "PUT" }
        };
    }
}