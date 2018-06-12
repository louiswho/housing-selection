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
        private readonly IDbContext _HousingSelectionDbContext;
    
        List<Batch> batches;

        public BatchRepository(IDbContext housingSelectionContext)
        {
            _HousingSelectionDbContext = housingSelectionContext;

        }
        public BatchRepository(HousingSelectionDbContext housingSelectionDbContext)
        {
            //housingSelectionDbContext = _HousingSelectionDbContext;

        }

        public void AddBatch(Batch batch)
        {
             _HousingSelectionDbContext.Batches.Add(batch);
           
        }

        public IEnumerable<Batch> GetBatches()
        {

            return _HousingSelectionDbContext.Batches;
        }

        public Batch GetBatchById(Guid id)
        {
            return _HousingSelectionDbContext.Batches.First(x => x.Id == id);
        }

        public Batch GetBatchByBatchId(Guid batchId)
        {
            return _HousingSelectionDbContext.Batches.First(x => x.BatchId == batchId);
        }


        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

     
    }
}
