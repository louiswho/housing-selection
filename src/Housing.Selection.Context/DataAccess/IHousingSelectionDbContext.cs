
using Housing.Selection.Library;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Selection.Context.DataAccess
{
    public interface IHousingSelectionDbContext
    {
        int saveChanges();
        DbSet<Room> Rooms {get; set;}
        DbSet<User> Users { get; set; }
        DbSet<Batch> Batches { get; set; }
        DbSet<Address> Addresses { get; set; }
        DbSet<Name> Names { get; set; }
    }
}
