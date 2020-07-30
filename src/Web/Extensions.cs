using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.DependencyInjection;

namespace Arbetsprov.Web
{
    public static class Extensions
    {
        public static string FrontendFolder = "Frontend";
        public static string BuildFolder = "Build";

        public static void AddSpa(this IServiceCollection services)
        {
            services.AddSpaStaticFiles(cfg => cfg.RootPath = $"{FrontendFolder}/{BuildFolder}");
        }

        public static void UseSpa(this IApplicationBuilder application, bool development)
        {
            application.UseSpaStaticFiles();
            application.UseSpa(spa =>
            {
                spa.Options.SourcePath = FrontendFolder;
                if (development)
                    spa.UseReactDevelopmentServer(npmScript: "start");
            });
        }
    }
}
