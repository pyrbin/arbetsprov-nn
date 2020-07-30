using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Arbetsprov.Infrastructure.Data;
using Arbetsprov.Application.Interfaces;

namespace Arbetsprov.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                // In memory database context
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase("ArbetsprovInMemoryDb");
                    options.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                });
            }
            else
            {
                // Database context
                services.AddDbContext<DataContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(DataContext).Assembly.FullName)));
            }

            services.AddScoped<IDataContext>(provider => provider.GetService<DataContext>());
        }
    }
}