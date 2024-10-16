using EmployeeManagementSystem.Core;
using EmployeeManagementSystem.Infrastructure;
using EmployeeManagementSystem.Infrastructure.Data;
using EmployeeManagementSystem.Web.Mappings;
using EmployeeManagementSystem.Web.Middlewares;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:3000",
                "http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services
    .AddCoreDependencyInjection(builder.Configuration)
    .AddInfrastructureDependencyInjection(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options
        .SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Employee management system",
            Version = "v1"
        });
});
builder.Services.AddAutoMapper(typeof(EmployeeMapping));

builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.SetMinimumLevel(LogLevel.Trace);
});

builder.Services.AddSingleton<ILoggerProvider, NLogLoggerProvider>();

var app = builder.Build();

var logger = NLog.LogManager.GetCurrentClassLogger();
using (var scope = app.Services.CreateScope())
{
    try
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await ApplicationDbContextSeed.SeedAsync(dbContext);
    }
    catch (Exception ex)
    {
        logger.Error(ex, "An error occurred seeding the DB.");
    }
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee management system");
    });

app.Run();
