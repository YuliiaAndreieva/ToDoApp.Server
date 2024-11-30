using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.BLL.DTO;

namespace ToDoApp.BLL.Configuration;

public static class ServiceRegistrations
{
    public static IServiceCollection AddBllServices(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssemblyContaining<CreateUserTaskDto>();
        return services;
    }
}