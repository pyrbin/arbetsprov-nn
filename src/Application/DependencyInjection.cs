using Arbetsprov.Application.Interfaces;
using Arbetsprov.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Arbetsprov.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IPriceDetailService, PriceDetailService>();
        }
    }
}
