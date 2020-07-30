using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arbetsprov.Application.Interfaces;
using Arbetsprov.Application.DTO;
using Arbetsprov.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Arbetsprov.Application.Services
{
    public class PriceDetailService : IPriceDetailService
    {
        private readonly IDataContext Context;
        private readonly IOptimizedPriceGetter PriceGetter;

        public PriceDetailService(IDataContext context)
        {
            Context = context;
            PriceGetter = new OptimizedPriceGetter();
        }

        public async Task<IEnumerable<OptimizedPricePeriod>> GetOptimizedPeriodFor(string sku, string currency, string market)
        {
            var priceDetails = await Context.PriceDetails
                .Where(pd => pd.CatalogEntryCode == sku && pd.CurrencyCode == currency && pd.MarketId == market)
                .ToListAsync();

            return PriceGetter.Calculate(new OptimizedPriceOptions() {
                Currency = currency, 
                Market = market,
                Prices = priceDetails
            });
        }
    }
}
