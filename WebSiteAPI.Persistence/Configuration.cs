using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace WebSiteAPI.Persistence
{
    static class Configuration
    {
        public static string ConnectionString
        {
            get
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                return configuration.GetConnectionString("MsSQL");
            }
        }
    }
}