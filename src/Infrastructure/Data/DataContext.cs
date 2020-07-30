using System.Reflection;
using Arbetsprov.Application.Interfaces;
using Arbetsprov.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arbetsprov.Infrastructure.Data
{
    /// <summary>
    /// Application specific EFCore database context.
    /// </summary>
    public sealed class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<PriceDetail> PriceDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
