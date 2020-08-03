using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Arbetsprov.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Arbetsprov.Web.API
{
    public sealed class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    // Seed & migrate database if needed
                    var ctx = scope.ServiceProvider.GetRequiredService<DataContext>();
                    await ctx.Database.MigrateAsync();
                    await DataContextSeed.SeedAsync(ctx);
                }
                catch (Exception ex)
                {
                    // TODO: Add better logging system
                    Debug.WriteLine(ex);
                }
            }

            await host.RunAsync();
        }

        // EF Core uses this method at design time to access the DbContext
        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(
                    webBuilder => webBuilder.UseStartup<Startup>());
    }
}
