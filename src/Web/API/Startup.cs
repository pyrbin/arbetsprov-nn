using Arbetsprov.Application;
using Arbetsprov.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Arbetsprov.Web.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        public void Configure(IApplicationBuilder application, IWebHostEnvironment env)
        {
            application.UseHttpsRedirection();
            application.UseBlazorFrameworkFiles();
            application.UseStaticFiles();

            application.UseRouting();
            application.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapFallbackToFile("index.html");
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(DbConnectionString, UseInMemoryDb);
            services.AddControllers();
        }

        public IConfiguration Configuration { get; }
        public string DbConnectionString => Configuration.GetConnectionString("DefaultConnection");
        public bool UseInMemoryDb => Configuration.GetValue<bool>("UseInMemoryDatabase");

    }
}
