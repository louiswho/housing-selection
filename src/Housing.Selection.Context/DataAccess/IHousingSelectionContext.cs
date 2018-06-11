
using Housing.Selection.Library;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Housing.Selection.Library.HousingModels;

namespace Housing.Selection.Context.DataAccess
{
    public interface IHousingSelectionContext
    {
        int saveChanges();
        DbSet<Room> GetRooms();
        DbSet<Batch> GetBatches();
        DbSet<User> GetUsers();
        DbSet<Address> GetAddress();
        DbSet<Name> GetNames();



    }
}
