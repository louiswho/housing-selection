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
        //change to _housingSelectionDbContext;
        

        public BatchRepository(IDbContext housingSelectionContext)
        {
            _HousingSelectionDbContext = housingSelectionContext;
            //Clean up space.
        }
        public BatchRepository(HousingSelectionDbContext housingSelectionDbContext)
        {
            //housingSelectionDbContext = _HousingSelectionDbContext;
            //Remove this method.
        }

        public void AddBatch(Batch batch)
        {
             _HousingSelectionDbContext.Batches.Add(batch);
            //Clean up space.
        }

        public IEnumerable<Batch> GetBatches()
        {
            //Clean up space.
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
            _HousingSelectionDbContext.saveChanges();
        }

        //Collapse space.
    }
}
