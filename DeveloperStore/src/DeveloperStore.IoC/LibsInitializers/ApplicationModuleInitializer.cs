using DeveloperStore.Application.Sales.Interfaces;
using DeveloperStore.Application.Sales.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperStore.IoC.LibsInitializers;

public class ApplicationModuleInitializer : ILibrariesInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ISaleService, SaleService>();
    }
}