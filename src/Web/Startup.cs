using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Arbetsprov.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        public void Configure(IApplicationBuilder application, IWebHostEnvironment env)
        {
            application.UseHttpsRedirection();
            application.UseStaticFiles();
            application.UseRouting();
            application.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller}/{action=Index}/{id?}");
            });
            application.UseSpa(env.IsDevelopment());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSpa();
        }

        public IConfiguration Configuration { get; }
        public string DbConnectionString => Configuration.GetConnectionString("DefaultConnection");
    }
}
