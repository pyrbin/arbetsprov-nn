using System;
using System.Collections.Generic;
using System.Linq;
using Arbetsprov.Application.DTO;
using Arbetsprov.Application.Interfaces;
using Arbetsprov.Core.Entities;

namespace Arbetsprov.Application.Services
{

    /// <summary>
    /// Implementation of IOptimizedPriceGetter to calculate optimized prices for a
    /// certain product in a certain market with a certain currency.
    /// </summary>
    public class OptimizedPriceGetter : IOptimizedPriceGetter
    {
        // TODO: Add some better (performance, readability) data structures?
        private List<OptimizedPricePeriod> Output;
        private List<PriceDetail> OpenSet;
        // This could possibly be some sort of MaxHeap / PriorityQueue?
        private List<PriceDetail> ActiveCandidates;

        private DateTime LastDate;

        public IEnumerable<OptimizedPricePeriod> Calculate(OptimizedPriceOptions options)
        {
            Output = new List<OptimizedPricePeriod>();
            OpenSet = options.Prices;
            ActiveCandidates = new List<PriceDetail>();

            // Return empty list if prices are empty
            if (OpenSet.Count <= 0)
            {
                return Output;
            }

            // Replace null values with MaxValue for the algorithm
            foreach (var item in OpenSet.Where(x => x.ValidUntil == null))
                item.ValidUntil = DateTime.MaxValue;

            // TODO: this sorting could be moved to the database query
            // to let the database handle the sorting (probly faster)
            SortEarliestDateFirst();

            LastDate = OpenSet[0].ValidFrom;

            while (OpenSet.Count > 0)
            {
                var current = OpenSet[0];
                var removeCurrent = true;
                var data = new OptimizedPricePeriod()
                {
                    Price = current.UnitPrice,
                    Start = LastDate,
                    End = (DateTime)current.ValidUntil,
                    Market = options.Market,
                    Currency = options.Currency
                };

                // If we have gap between current date and last date,
                // check in ActiveCandidates to fill the gap
                if (current.ValidFrom > LastDate && ActiveCandidates.Count != 0)
                {
                    RemoveExpiredCandiates();
                    GetLowestPriceFromCandidates(out var lowest);

                    // If lowest can fill whole gap, use currents starting date, else use lowests.
                    data.End = lowest.ValidUntil < current.ValidFrom ? lowest.ValidUntil : current.ValidFrom;

                    data.Price = lowest.UnitPrice;
                    removeCurrent = false;
                }
                // If we're not at last price
                else if (OpenSet.Count > 1)
                {
                    var next = OpenSet[1];

                    // If next price is higher than this, remove from open list
                    if (data.Price < next.UnitPrice)
                    {
                        // If next price will still be valid after current has expired
                        // add to active candidates
                        if (next.ValidUntil > current.ValidUntil)
                            ActiveCandidates.Add(next);
                        OpenSet.RemoveAt(1);
                        continue;
                    }
                    // If next item in OpenSet is valid & price is lower
                    else if (next.ValidFrom < current.ValidUntil)
                    {
                        data.End = next.ValidFrom;
                        // If current item still is valid after next has expired,
                        // add to active candidates
                        if (next.ValidUntil < current.ValidUntil)
                            ActiveCandidates.Add(current);
                    }
                }
                // If we're at last price in list
                else
                {
                    RemoveExpiredCandiates();

                    OpenSet.RemoveAt(0);

                    // Add all active candidates to the OpenSet
                    OpenSet.AddRange(ActiveCandidates);

                    // Sort by latest date first
                    // We do this because
                    SortLatestDateFirst();

                    ActiveCandidates.Clear();
                    removeCurrent = false;
                }

                if (removeCurrent)
                    OpenSet.RemoveAt(0);

                LastDate = (DateTime)data.End;
                Output.Add(data);
            }

            return Output;
        }

        /// <summary>
        /// Remove all candiates that have expired, eg. their time period have passed.
        /// </summary>
        private void RemoveExpiredCandiates()
        {
            ActiveCandidates.RemoveAll(x => x.ValidUntil <= LastDate);
        }

        private void GetLowestPriceFromCandidates(out PriceDetail lowest)
        {
            var min = ActiveCandidates.Min(x => x.UnitPrice);
            lowest = ActiveCandidates.First(x => x.UnitPrice == min);
        }

        private void SortEarliestDateFirst()
        {
            OpenSet.Sort((a, b) => a.ValidFrom == b.ValidFrom
                ? a.UnitPrice.CompareTo(b.UnitPrice)
                : a.ValidFrom.CompareTo(b.ValidFrom));
        }

        private void SortLatestDateFirst()
        {
            OpenSet.Sort((a, b) => a.ValidFrom == b.ValidFrom
                ? a.UnitPrice.CompareTo(b.UnitPrice)
                : b.ValidFrom.CompareTo(a.ValidFrom));
        }
    }
}
