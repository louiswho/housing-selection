using System;
using System.Collections.Generic;
using System.Text;
using Housing.Selection.Library;
using Housing.Selection.Library.HousingModels;
using Microsoft.EntityFrameworkCore;

namespace Housing.Selection.Context.DataAccess
{
   public class HousingSelectionContext : IHousingSelectionContext
    {

        public HousingSelectionContext( IHousingSelectionContext housingSelectionDbContext)
        {
          

        }

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
