using Bogus;
using EmployeeManagementSystem.Core.Constants;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Core.Enumerations;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Infrastructure.Data;

public static class ApplicationDbContextSeed
{
    public static async Task SeedAsync(ApplicationDbContext context, int? retry = 0)
    {
        var retryForAvailability = retry.Value;
        try
        {
            context.Database.Migrate();

            if (!context.Employees.Any()) 
            {
                context.Employees.AddRange(GetGenerateEmployees());
            }

            await context.SaveChangesAsync();
        }
        catch (Exception)
        {
            if (retryForAvailability < 3)
            {
                retryForAvailability++;
                await SeedAsync(context, retryForAvailability);
            }
            throw;
        }
    }

    private static IEnumerable<Employee> GetGenerateEmployees()
    {
        var faker = new Faker<Employee>()
            .RuleFor(e => e.FirstName, f => f.Name.FirstName())
            .RuleFor(e => e.LastName, f => f.Name.LastName())
            .RuleFor(e => e.Age, f => f.Random.Int(AgeConstants.MinAge, AgeConstants.MaxAge))
            .RuleFor(e => e.Sex, f => f.PickRandom<Sex>());

        return faker.Generate(10);
    }
}