using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Housing.Selection.Library.HousingModels;
using Microsoft.EntityFrameworkCore;

namespace Housing.Selection.Context.DataAccess
{
    public class BatchRepository : IBatchRepository
    {
        private readonly IDbContext _housingSelectionDbContext;

        public BatchRepository(IDbContext housingSelectionContext)
        {
            _housingSelectionDbContext = housingSelectionContext;
        }

        public void AddBatch(Batch batch)
        {
            _housingSelectionDbContext.Batches.Add(batch);
            _housingSelectionDbContext.SaveChanges();
        }

        public IEnumerable<Batch> GetBatches()
        {
            return _housingSelectionDbContext.Batches
                .Include(x => x.Users);
        }

        public async Task<Batch> GetBatchById(Guid id)
        {
            return await _housingSelectionDbContext.Batches
                .Include(x => x.Users)
                .FirstAsync(x => x.Id == id);
        }

        public async Task<Batch> GetBatchByBatchId(Guid batchId)
        {
            return await _housingSelectionDbContext.Batches
                .Include(x => x.Users)
                .FirstAsync(x => x.BatchId == batchId);
        }

        public async Task SaveChangesAsync()
        {
            await _housingSelectionDbContext.SaveChangesAsync();
        }
    }
}
