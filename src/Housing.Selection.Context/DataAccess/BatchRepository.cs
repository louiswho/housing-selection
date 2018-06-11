using System;
using System.Collections.Generic;
using System.Linq;
using Housing.Selection.Library;

namespace Housing.Selection.Context.DataAccess
{
    /// <summary>
    /// Add, read, update and get by Id Revature batches from Housing-Selection database.
    /// </summary>
    

    public class BatchRepository : IBatchRepository
    {
        private List<Batch> batches = new List<Batch>();

        public BatchRepository()
        {
          

        }

        public void AddBatch(Batch batch)
        {
            batches.Add(batch);
           
        }

        public IEnumerable<Batch> GetBatches()
        {

            return batches;
        }

        public Batch GetBatchById(Guid id)
        {
            return batches.First(x => x.Id == id);
        }

        public Batch GetBatchByBatchId(Guid batchId)
        {
            return batches.First(x => x.BatchId == batchId);
        }


        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

     
    }
}
