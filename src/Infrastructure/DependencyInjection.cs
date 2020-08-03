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
        public static void AddInfrastructure(this IServiceCollection services, string connectionString, bool useInMemoryDb = false)
        {
            if (useInMemoryDb)
            {
                // In memory database context
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDb");
                    options.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                });
            }
            else
            {
                // Database context
                services.AddDbContext<DataContext>(opts => opts.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(DataContext).Assembly.FullName)));
            }

            services.AddScoped<IDataContext>(provider => provider.GetService<DataContext>());
        }
    }
}