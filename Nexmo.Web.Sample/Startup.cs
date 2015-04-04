using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Nexmo.Web.Sample.Startup))]
namespace Nexmo.Web.Sample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var webApiConfiguration = ConfigureWebApi();

            // Use the extension method provided by the WebApi.Owin library:
            app.UseWebApi(webApiConfiguration);
        }

        private HttpConfiguration ConfigureWebApi()
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
            return config;
        }
    }
}
