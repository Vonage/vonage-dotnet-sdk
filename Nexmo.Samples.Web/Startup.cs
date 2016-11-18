using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Nexmo.Samples.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddMemoryCache();
        }

        public void Configure(IApplicationBuilder app)
        {
            // Display custom error page in production when error occurs
            // During development use the ErrorPage middleware to display error information in the browser
            // TODO: IHostingEnvironment is deprecated - https://github.com/aspnet/PlatformAbstractions/issues/37
            // if (env.IsDevelopment())
            // {
                app.UseDeveloperExceptionPage();
            // }

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}