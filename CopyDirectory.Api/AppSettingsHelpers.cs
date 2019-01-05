using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace CopyDirectory.Api
{
    public static class AppSettingsHelpers
    {
        public static IConfiguration GetConfiguration(bool reloadConfigOnChange = false)
        {
            return GetConfigurationFromFile("appsettings.json", reloadConfigOnChange);
        }

        public static IConfiguration GetConfigurationFromFile(string fileName)
        {
            return GetConfigurationFromFile(fileName, false);
        }

        public static IConfiguration GetConfigurationFromFile(string fileName, bool reloadConfigOnChange)
        {
            if (String.IsNullOrWhiteSpace(fileName))
            {
                return GetConfiguration(reloadConfigOnChange);
            }
            var configHelper = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(fileName, optional: false, reloadOnChange: reloadConfigOnChange)
                .AddEnvironmentVariables()
                .Build();
            return configHelper;
        }
    }
}
