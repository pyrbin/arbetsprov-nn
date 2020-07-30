using Arbetsprov.Core.Entities;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Globalization;

namespace Arbetsprov.Infrastructure.Data
{
    public sealed class PriceDetailConfiguration : IEntityTypeConfiguration<PriceDetail>
    {
        /// <summary>
        /// Configures PriceDetail database properties.
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<PriceDetail> builder)
        {
            builder.ToTable("PriceDetail");
            builder.HasKey(i => i.PriceValueId);       // Primary key
            builder.HasIndex(i => i.CatalogEntryCode); // Index key
        }
    }
}
