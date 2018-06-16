using Housing.Selection.Library.HousingModels;
using Microsoft.EntityFrameworkCore;

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
    }
}
