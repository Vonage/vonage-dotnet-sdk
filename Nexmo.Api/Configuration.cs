using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Nexmo.Api
{
    public sealed class Configuration
    {
        private static readonly Configuration instance = new Configuration();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Configuration()
        {
        }

        private Configuration()
        {
            var builder = new ConfigurationBuilder()
            //.SetBasePath(env.ContentRootPath)
            .AddJsonFile("settings.json", false, true);

            Settings = builder.Build();
        }

        public static Configuration Instance
        {
            get
            {
                return instance;
            }
        }

        public IConfiguration Settings { get; private set; }
    }
}
