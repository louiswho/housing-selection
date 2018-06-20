using System.Collections.Generic;
using System.Threading.Tasks;
using Housing.Selection.Context.DataAccess;
using Housing.Selection.Context.HttpRequests;
using Housing.Selection.Library.HousingModels;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Context.Polling
{
    /// <summary>
    /// Polls the Service Hub Batch database, and updates our(housing) Batch database
    /// With the data returned to ensure that our DB is up to date with Service Hubs
    /// </summary>
    public class PollBatch : IPollBatch
    {
        private readonly IBatchRepository _batchRepository;
        private readonly IServiceBatchCalls _batchRetrieval;

        public PollBatch(IBatchRepository batchRepository, IServiceBatchCalls batchRetrieval)
        {
            _batchRepository = batchRepository;
            _batchRetrieval = batchRetrieval;
        }
        public async Task<List<Batch>> BatchPollAsync()
        {
            var batchList = new List<Batch>();
            var batches = await _batchRetrieval.RetrieveAllBatchesAsync();
            if (batches == null)
                return batchList;

            foreach (var batch in batches)
            {
                batchList.Add(await UpdateBatchAsync(batch));
            }
            return batchList;

        }

        /// <summary>
        /// Updates a single Batch in the housing Batch database based on the Batch data retrieved from the service hub database
        /// </summary>
        /// <param name="apiBatch">
        /// The ApiBatch object retrieved from the BatchRetireval
        /// Contains the properties to update housing's matching Batch with
        /// </param>        
        /// <returns>
        /// Returns a Batch that contains the updated properties
        /// </returns>
        public async Task<Batch> UpdateBatchAsync(ApiBatch apiBatch)
        {
            var housingBatch = await _batchRepository.GetBatchByBatchId(apiBatch.BatchId);

            if (housingBatch == null)
            {
                var batch = new Batch();
                _batchRepository.AddBatch(batch.ConvertFromServiceModel(apiBatch));
                return batch;
            }

            housingBatch = housingBatch.ConvertFromServiceModel(apiBatch: apiBatch);
            await _batchRepository.SaveChangesAsync();
            return housingBatch;
        }
    }
}