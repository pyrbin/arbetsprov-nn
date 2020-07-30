using System;

namespace Arbetsprov.Application.DTO
{
    public class OptimizedPricePeriod
    {
        public string Market { get; set; }
        public float Price { get; set; }
        public string Currency { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}
