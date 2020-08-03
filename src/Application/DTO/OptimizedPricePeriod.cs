using System;

namespace Arbetsprov.Application.DTO
{
    /// <summary>
    /// Data object/class for optimized price periods
    /// </summary>
    public class OptimizedPricePeriod
    {
        public string Market { get; set; }
        public float Price { get; set; }
        public string Currency { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}
