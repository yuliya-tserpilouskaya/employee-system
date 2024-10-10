using EmployeeManagementSystem.Core.BusinessLogic;
using EmployeeManagementSystem.Core.Interfaces;
using EmployeeManagementSystem.Infrastructure.Data;
using EmployeeManagementSystem.Web.Mappings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("EMSDatabase");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString,
    builderOptions => builderOptions.MigrationsAssembly("EmployeeManagementSystem.Infrastructure")));

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(GenericRepository<>));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(EmployeeMapping));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    //try
    //{ TODO: logger
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await ApplicationDbContextSeed.SeedAsync(dbContext);
    //}
    //catch (Exception ex)
    //{
    //    logger.Error(ex, "An error occurred seeding the DB.");
    //}

    
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
