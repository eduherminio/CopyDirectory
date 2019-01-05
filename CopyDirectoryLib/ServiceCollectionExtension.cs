using CopyDirectoryLib.Service;
using CopyDirectoryLib.Service.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace CopyDirectoryLib
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCopyDirectoryServices(this IServiceCollection services)
        {
            return services.AddScoped<ICopyService, CopyService>();
        }
    }
}
