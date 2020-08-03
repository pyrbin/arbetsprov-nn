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
    /// <summary>
    /// PriceDetail service class
    /// </summary>
    public class PriceDetailService : IPriceDetailService
    {
        private readonly IDataContext Context;
        private readonly IOptimizedPriceGetter PriceGetter;

        public PriceDetailService(IDataContext context)
        {
            Context = context;
            PriceGetter = new OptimizedPriceGetter();
        }

        /// <summary>
        /// Get optimized price periods for specified SKU.
        /// </summary>
        /// <param name="sku"></param>
        /// <param name="currency"></param>
        /// <param name="market"></param>
        /// <returns></returns>
        public async Task<IEnumerable<OptimizedPricePeriod>> GetOptimizedPeriodFor(string sku, string currency, string market)
        {
            var priceDetails = await Context.PriceDetails
                .Where(pd => pd.CatalogEntryCode == sku && pd.CurrencyCode == currency && pd.MarketId == market)
                .ToListAsync();

            return PriceGetter.Calculate(new OptimizedPriceOptions()
            {
                Currency = currency,
                Market = market,
                Prices = priceDetails
            });
        }

        /// <summary>
        /// Get all available currencies for specified SKU.
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetCurrenciesFor(string sku)
        {
            return await Context.PriceDetails
                .Where(pd => pd.CatalogEntryCode == sku)
                .Select(pd => pd.CurrencyCode)
                .Distinct()
                .ToListAsync();
        }

        /// <summary>
        /// Get all available markets for specified SKU
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetMarketsFor(string sku)
        {
            return await Context.PriceDetails
                .Where(pd => pd.CatalogEntryCode == sku)
                .Select(pd => pd.MarketId)
                .Distinct()
                .ToListAsync();
        }

        /// <summary>
        /// If specified SKU exists in the database
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        public async Task<bool> Exists(string sku)
        {
            return await Context.PriceDetails.AnyAsync(e => e.CatalogEntryCode == sku);
        }
    }

}