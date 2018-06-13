using Housing.Selection.Library.HousingModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Selection.Context.DataAccess
{
    public interface IDbContext
    {
        int saveChanges(); //Change to SaveChanges
        DbSet<Room> Rooms { get; set; }
        DbSet<Batch> Batches { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Address> Addresses { get; set; }
        DbSet<Name> Names { get; set; }

        //Clean up space around class and remove unnessary namespaces. 
    }
}
