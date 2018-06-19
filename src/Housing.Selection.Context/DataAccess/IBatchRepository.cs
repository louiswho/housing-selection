using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Housing.Selection.Library.HousingModels;

namespace Housing.Selection.Context.DataAccess
{
    public interface IBatchRepository
    {
        IEnumerable<Batch> GetBatches();
        Task<Batch> GetBatchById(Guid id);
        Task<Batch> GetBatchByBatchId(Guid batchId);
        void AddBatch(Batch batch);
        Task SaveChanges();
    }
}
