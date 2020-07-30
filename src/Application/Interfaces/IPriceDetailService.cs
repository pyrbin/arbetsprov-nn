using System.Collections.Generic;
using System.Threading.Tasks;

using Arbetsprov.Application.DTO;
using Arbetsprov.Core.Entities;

namespace Arbetsprov.Application.Interfaces
{
    public interface IPriceDetailService
    {
        Task<IEnumerable<OptimizedPricePeriod>> GetOptimizedPeriodFor(string sku, string currency, string market);
    }
}
