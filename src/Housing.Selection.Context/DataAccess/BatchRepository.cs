using System;
using System.Collections.Generic;
using System.Linq;
using Housing.Selection.Library;
using Housing.Selection.Library.HousingModels;

namespace Housing.Selection.Context.DataAccess
{
    /// <summary>
    /// Add, read, update and get by Id Revature batches from Housing-Selection database.
    /// </summary>
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
        }

        public IEnumerable<Batch> GetBatches()
        {
            return _housingSelectionDbContext.Batches;
        }

        public Batch GetBatchById(Guid id)
        {
            return _housingSelectionDbContext.Batches.First(x => x.Id == id);
        }

        public Batch GetBatchByBatchId(Guid batchId)
        {
            return _housingSelectionDbContext.Batches.First(x => x.BatchId == batchId);
        }


        public void SaveChanges()
        {
            _housingSelectionDbContext.saveChanges();
        }
    }
}
