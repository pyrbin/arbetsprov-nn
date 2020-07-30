using Arbetsprov.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arbetsprov.Application.Interfaces
{
    public interface IDataContext
    {
        DbSet<PriceDetail> PriceDetails { get; set; }
    }
}
