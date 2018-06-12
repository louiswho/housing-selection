using Housing.Selection.Library;
using System;
using System.Collections.Generic;
using System.Text;
using Housing.Selection.Library.HousingModels;

namespace Housing.Selection.Context.DataAccess
{
    /// <summary>
    /// Creates, reads and updates  Revature batches from Housing-Selection database.
    /// </summary>
    public interface IBatchRepository
    {
        IEnumerable<Batch> GetBatches();
        Batch GetBatchById(Guid id);
        Batch GetBatchByBatchId(Guid batchId);
        void AddBatch(Batch batch);
        void SaveChanges();
    }
}
