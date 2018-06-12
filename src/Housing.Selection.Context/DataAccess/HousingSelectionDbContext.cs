using Housing.Selection.Library;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Housing.Selection.Library.HousingModels;

namespace Housing.Selection.Context.DataAccess
{
   public class HousingSelectionDbContext : DbContext, IHousingSelectionContext
    {

        public HousingSelectionDbContext() : base()
        {

        }

        public DbSet<User> Users { get; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Name> Names { get; set; }

        public DbSet<Address> GetAddress()
        {
            throw new NotImplementedException();
        }

        public DbSet<Batch> GetBatches()
        {
            throw new NotImplementedException();
        }

        public DbSet<Name> GetNames()
        {
            throw new NotImplementedException();
        }

        public DbSet<Room> GetRooms()
        {
            throw new NotImplementedException();
        }

        public DbSet<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public int saveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
