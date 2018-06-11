using Housing.Selection.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Selection.Context.Interfaces
{
    /// <summary>
    /// Creates, reads and updates  Revature batches from Housing-Selection database.
    /// </summary>
    public interface IBatchRepository
    {
        IEnumerable<Batch> GetBatches();
        Batch GetBatchById(Guid id);
        void AddBatch(Batch batch);
        void SaveChanges();
    }
}
