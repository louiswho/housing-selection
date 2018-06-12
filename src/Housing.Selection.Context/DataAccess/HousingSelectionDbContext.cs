using Housing.Selection.Library;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Housing.Selection.Library.HousingModels;

namespace Housing.Selection.Context.DataAccess
{
   public class HousingSelectionDbContext :    DbContext, IDbContext
    {

        public HousingSelectionDbContext() : base()
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Name> Names { get; set; }


        public int saveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
