using Housing.Selection.Library.HousingModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Housing.Selection.Context.DataAccess
{
   public class NameRepository : INameRepository
    {
        private readonly IDbContext _housingSelectionDbContext;

        public NameRepository(IDbContext housingSelectionContext)
        {
            _housingSelectionDbContext = housingSelectionContext;
        }

        public void AddName(Name name)
        {
            _housingSelectionDbContext.Names.Add(name);
        }

        public IEnumerable<Name> GetNames()
        {
            return _housingSelectionDbContext.Names;
        }

        public Name GetNameById(Guid id)
        {
            return _housingSelectionDbContext.Names.First(x => x.Id == id);
        }

        public Name GetNameByNameId(Guid nameId)
        {
            return _housingSelectionDbContext.Names.First(x => x.NameId == nameId);
        }

        public void SaveChanges()
        {
            _housingSelectionDbContext.SaveChanges();
        }
    }
}
