using Microsoft.EntityFrameworkCore;
using Housing.Selection.Library.HousingModels;
using System.Threading.Tasks;

namespace Housing.Selection.Context.DataAccess
{
   public class HousingSelectionDbContext : DbContext, IDbContext
    {
        public HousingSelectionDbContext() : base()
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Name> Names { get; set; }

     
    }
}
