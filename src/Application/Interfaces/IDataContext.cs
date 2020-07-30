using Arbetsprov.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Arbetsprov.Application.Interfaces
{
    public interface IDataContext
    {
        DbSet<PriceDetail> PriceDetails { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
