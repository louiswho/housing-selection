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

        public Batch UpdateBatch(ApiBatch batch)
        {
            
            var housingBatch = batchRepository.GetBatchByBatchId(batch.BatchId);
            housingBatch = housingBatch.ConvertFromServiceModel(apiBatch: batch);
            batchRepository.SaveChanges();
            return housingBatch;
        }
    }
}