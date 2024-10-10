using EmployeeManagementSystem.Core.Interfaces;
using EmployeeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace EmployeeManagementSystem.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("EMSDatabase"),
         builderOptions => builderOptions.MigrationsAssembly("EmployeeManagementSystem.Infrastructure")));

        services.AddScoped(typeof(IAsyncRepository<>), typeof(GenericRepository<>));

        return services;
    }
}