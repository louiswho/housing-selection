using System;
using System.Collections.Generic;
using Housing.Selection.Library.HousingModels;

namespace Housing.Selection.Context.DataAccess
{
    public interface IBatchRepository
    {
        IEnumerable<Batch> GetBatches();
        Batch GetBatchById(Guid id);
        Batch GetBatchByBatchId(Guid batchId);
        void AddBatch(Batch batch);
        void SaveChanges();
    }
}
