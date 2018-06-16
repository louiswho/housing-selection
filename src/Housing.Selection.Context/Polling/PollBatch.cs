using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Housing.Selection.Context.DataAccess;
using Housing.Selection.Context.HttpRequests;
using Housing.Selection.Library.HousingModels;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Context.Polling
{
    public class PollBatch : IPollBatch
    {
        private IBatchRepository batchRepository;
        private IServiceBatchCalls batchRetrieval;

        public PollBatch(IBatchRepository batchRepository, IServiceBatchCalls batchRetrieval)
        {
            this.batchRepository = batchRepository;
            this.batchRetrieval = batchRetrieval;
        }
        public async Task<List<Batch>> BatchPoll()
        {
            var batchList = new List<Batch>();
            var batches = await batchRetrieval.RetrieveAllBatchesAsync();
            if (batches != null)
            {
                foreach (var batch in batches)
                {
                    batchList.Add(UpdateBatch(batch));
                }
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
        public Batch UpdateBatch(ApiBatch apiBatch)
        {            
            var housingBatch = batchRepository.GetBatchByBatchId(apiBatch.BatchId);
            housingBatch = housingBatch.ConvertFromServiceModel(apiBatch: apiBatch);
            batchRepository.SaveChanges();
            return housingBatch;
        }
    }
}