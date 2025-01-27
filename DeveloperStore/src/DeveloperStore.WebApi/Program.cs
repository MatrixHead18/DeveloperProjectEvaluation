using DeveloperStore.Application;
using DeveloperStore.IoC;
using DeveloperStore.IoC.Extensions;
using DeveloperStore.ORM;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DeveloperStore.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Information("Starting web application");

        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        AddingServices(builder);

        var app = builder.Build();

        UseServices(app);

        app.Run();
    }

    private static void AddingServices(WebApplicationBuilder builder)
    {
        builder.AddDefaultLogging();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.AddBasicHealthChecks();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<SaleDbContext>(options =>
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("DeveloperStore.ORM")
            ).EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine)
        );

        builder.RegisterDependencies();

        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(
                typeof(ApplicationLayer).Assembly,
                typeof(Program).Assembly
            );
        });
    }

    private static void UseServices(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod());

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseBasicHealthChecks();

        app.MapControllers();
    }
}
