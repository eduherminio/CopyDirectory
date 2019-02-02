using System;
using System.Threading.Tasks;
using CopyDirectory.Api.Constants;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace CopyDirectory.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                IConfiguration configuration = GetConfiguration();
                Console.WriteLine("Starting CopyDirectory.Api");
                Console.WriteLine($"API can be consulted at {configuration[AppSettingsKeys.PublicUrlConfigurationKey]}/swagger");

                Task publicApi = BuildWebHost(args, configuration, AppSettingsKeys.PublicUrlConfigurationKey)
                    .RunAsync();

                Task.WaitAll(publicApi);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Stopped program because of exception\n{e}");
                throw;
            }
        }

        private static IConfiguration GetConfiguration()
        {
            return AppSettingsHelpers.GetConfigurationFromFile(AppSettingsKeys.ConfigurationFile);
        }

        public static IWebHost BuildWebHost(string[] args, IConfiguration configuration, string urlKey)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseConfiguration(configuration)
                .UseStartup<Startup>()
                .UseUrls(configuration[urlKey])
                .Build();
        }
    }
}
