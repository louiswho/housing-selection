using System.Collections.Generic;
using Housing.Selection.Library;
using Housing.Selection.Context.DataAccess;
using Housing.Selection.Context.HttpRequests;

namespace Housing.Selection.Context.Polling
{
    /// <summary>
    ///  Polls Service Hubs database and updates our records accordingly
    /// </summary>
    public class PollingService : IPollingService
    {
        private IHttpClientWrapper httpClient;
        private IBatchRepository batchRepository;
        public PollingService(IHttpClientWrapper httpClient, IBatchRepository batchRepository)
        {
            this.httpClient = httpClient;
            this.batchRepository = batchRepository;
        }
        /// <summary>
        ///  Poll all Service Hub databases through their respective API's and updates our databases with the new data 
        /// </summary>
        public void PollAll()
        {
            var batches = httpClient.GetBatches();
            foreach (var batch in batches)
            {
                UpdateBatch(batch)
            }
            var rooms = httpClient.GetRooms();
            var users = httpClient.GetUsers();
        }
        private void UpdateBatch(ApiBatch batch)
        {
            
            var housingBatch = batchRepository.GetByBatchId(batch.BatchId);
            //TODO: Finish writing this method
        }
    }
}