using System.Collections.Generic;
using System.Threading.Tasks;

using Arbetsprov.Application.DTO;
using Arbetsprov.Core.Entities;

namespace Arbetsprov.Application.Interfaces
{
    public interface IOptimizedPriceGetter
    {
        IEnumerable<OptimizedPricePeriod> Calculate(OptimizedPriceOptions options);
    }

    public struct OptimizedPriceOptions
    {
        public string Market;
        public string Currency;
        public List<PriceDetail> Prices;
    }
}
