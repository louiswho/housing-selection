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
            _housingSelectionDbContext.SaveChanges();
        }

        public IEnumerable<Name> GetNames()
        {
            return _housingSelectionDbContext.Names
                .Include(x => x.Users);
        }

        public async Task<Name> GetNameById(Guid id)
        {
            return await _housingSelectionDbContext.Names
                .Include(x => x.Users)
                .FirstAsync(x => x.Id == id);
        }

        public async Task<Name> GetNameByNameId(Guid nameId)
        {
            return await _housingSelectionDbContext.Names
                .Include(x => x.Users)
                .FirstAsync(x => x.NameId == nameId);
        }

        public async Task SaveChangesAsync()
        {
            await _housingSelectionDbContext.SaveChangesAsync();
        }
    }
}
