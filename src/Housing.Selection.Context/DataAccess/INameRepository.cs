using Housing.Selection.Library.HousingModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Housing.Selection.Context.DataAccess
{
   public interface INameRepository
    {
        IEnumerable<Name> GetNames();
        Task<Name> GetNameById(Guid id);
        Task<Name> GetNameByNameId(Guid nameId);
        void AddName(Name batch);
        Task SaveChanges();
    }
}
