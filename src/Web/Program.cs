using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Arbetsprov.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Arbetsprov.Web
{
    public sealed class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<DataContext>();

                    // Do migration
                    if (context.Database.IsSqlServer())
                        context.Database.Migrate();

                    // Seed database with CSV values
                    await DataContextSeed.SeedCSV(context);
                }
                catch (Exception)
                {
                    // TODO: add a logger
                    Debug.WriteLine("An error occurred while migrating or seeding the database.");
                    throw;
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
