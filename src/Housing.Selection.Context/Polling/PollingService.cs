using Housing.Selection.Context.HttpRequests;
using Housing.Selection.Context.DataAccess;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Context.Polling
{
    /// <summary>
    ///  Polls Service Hubs database and updates our records accordingly
    /// </summary>
    public class PollingService : IPollingService
    {
        private IHttpClientWrapper httpClient;
        private IBatchRepository batchRepository;
        private IRoomRepository roomRepository;
        public PollingService(IHttpClientWrapper httpClient, IBatchRepository batchRepository, IRoomRepository roomRepository)
        {
            this.httpClient = httpClient;
            this.batchRepository = batchRepository;
            this.roomRepository = roomRepository;
        }
        /// <summary>
        ///  Poll all Service Hub databases through their respective API's and updates our databases with the new data 
        /// </summary>
        public void PollAll()
        {
            var batches = httpClient.GetBatches();
            foreach (var batch in batches)
            {
                UpdateBatch(batch);
            }
            var rooms = httpClient.GetRooms();
            foreach (var room in rooms)
            {
                UpdateRoom(room);
            }
            var users = httpClient.GetUsers();
        }
        private void UpdateBatch(ApiBatch batch)
        {            
            var housingBatch = batchRepository.GetBatchByBatchId(batch.BatchId);
            housingBatch = housingBatch.ConvertFromServiceModel(apiBatch: batch);
            batchRepository.UpdateBatch(housingBatch);            
        }

        private void UpdateRoom(ApiRoom room)
        {            
            var housingRoom = roomRepository.GetRoomByRoomId(room.RoomId);
            housingRoom = housingRoom.ConvertFromServiceModel(apiBatch: batch);
            roomRepository.UpdateRoom(housingRoom);            
        }
    }
}