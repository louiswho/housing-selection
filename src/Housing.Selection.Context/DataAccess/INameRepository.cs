using Housing.Selection.Library.HousingModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Selection.Context.DataAccess
{
   public interface INameRepository
    {
        IEnumerable<Name> GetNames();
        Name GetNameById(Guid id);
        Name GetNameByNameId(Guid nameId);
        void AddName(Name batch);
        void SaveChanges();
    }
}
