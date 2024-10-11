using EmployeeManagementSystem.Core.BusinessLogic;
using EmployeeManagementSystem.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagementSystem.Core;

public static class CoreDependencyInjection
{
    public static IServiceCollection AddCoreDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();
        
        return services;
    }
}