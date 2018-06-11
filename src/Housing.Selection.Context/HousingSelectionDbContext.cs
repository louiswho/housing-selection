using Housing.Selection.Library;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Selection.Context
{
   public class HousingSelectionDbContext : DbContext
    {

        public HousingSelectionDbContext() : base()
        {

        }
        public   DbSet<User> Users { get; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Name> Names { get; set; }
    }
}
