namespace ToDoApp.WebApi.Helpers;

public static class ResourceExtensions
{
    public static Resource<T> WithLinks<T>(this T data, int id)
    {
        return new Resource<T>(data)
        {
            Links = LinkBuilderHelper.CreateLinks(id)
        };
    }
}