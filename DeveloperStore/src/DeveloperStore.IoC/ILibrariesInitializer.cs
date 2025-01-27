using Microsoft.AspNetCore.Builder;

namespace DeveloperStore.IoC
{
    public interface ILibrariesInitializer
    {
        void Initialize(WebApplicationBuilder builder);
    }
}
