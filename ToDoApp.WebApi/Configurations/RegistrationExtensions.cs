using Microsoft.EntityFrameworkCore;
using ToDoApp.DAL.Data;
using ToDoApp.DAL.Repositories;

namespace ToDoApp.WebApi.Configurations;

public static class RegistrationExtensions
{
    public static void AddStorage(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (connectionString is null)
            throw new NullReferenceException(nameof(connectionString));

        serviceCollection.AddDbContext<ToDoAppDbContext>(options =>
        {
            options.UseSqlServer(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information);
        });

        serviceCollection.AddScoped<IUserTaskRepository, UserTaskRepository>();
    }
}