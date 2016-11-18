using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Nexmo.Samples.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();   

            host.Run();
        }
    }
}