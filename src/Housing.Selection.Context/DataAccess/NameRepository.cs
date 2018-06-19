using Housing.Selection.Library.HousingModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<Name> GetNameById(Guid id)
        {
            return await _housingSelectionDbContext.Names.FirstAsync(x => x.Id == id);
        }

        public async Task<Name> GetNameByNameId(Guid nameId)
        {
            return await _housingSelectionDbContext.Names.FirstAsync(x => x.NameId == nameId);
        }

        public async Task  SaveChanges()
        {
            await _housingSelectionDbContext.SaveChangesAsync();
        }
    }
}
