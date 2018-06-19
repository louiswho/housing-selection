using System.Collections.Generic;
using System.Threading.Tasks;
using Housing.Selection.Library.HousingModels;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Context.Polling
{
    public interface IPollBatch
    {
        Task<List<Batch>> BatchPollAsync();
        Task<Batch> UpdateBatchAsync(ApiBatch batch);
    }
}