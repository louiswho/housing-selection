using Housing.Selection.Library.HousingModels;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Housing.Selection.Context.DataAccess
{
    public interface IDbContext
    {
        int SaveChanges();
        DbSet<Room> Rooms { get; set; }
        DbSet<Batch> Batches { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Address> Addresses { get; set; }
        DbSet<Name> Names { get; set; }
        Task<int> SaveChangesAsync(CancellationToken ct = default(CancellationToken));
    }
}
