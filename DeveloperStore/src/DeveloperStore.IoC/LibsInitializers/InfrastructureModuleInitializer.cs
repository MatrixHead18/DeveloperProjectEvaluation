using DeveloperStore.Domain.Repositories;
using DeveloperStore.ORM;
using DeveloperStore.ORM.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperStore.IoC.LibsInitializers;

public class InfrastructureModuleInitializer : ILibrariesInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        //builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<SaleDbContext>());
        builder.Services.AddScoped<ISaleRepository, SaleRepository>();
    }
}