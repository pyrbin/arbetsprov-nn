using System;
namespace Arbetsprov.Core.Entities
{
    public sealed class PriceDetail
    {
        public long PriceValueId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string CatalogEntryCode { get; set; }
        public string MarketId { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidUntil { get; set; }
        public float UnitPrice { get; set; }
    }
}
