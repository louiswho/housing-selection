using System;
using System.Collections.Generic;
using System.Text;
using Housing.Selection.Library;
using Microsoft.EntityFrameworkCore;

namespace Housing.Selection.Context.DataAccess
{
   public class HousingSelectionContext : IHousingSelectionContext
    {

        public HousingSelectionContext( HousingSelectionDbContext housingSelectionDbContext)
        {
          

        }
    
        public DbSet<User> Users { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<Batch> Batches { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<Address> Addresses { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<Name> Names { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<Room> Rooms { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int saveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
