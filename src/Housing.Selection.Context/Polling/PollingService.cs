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
        private IUserRepository userRepository;
        public PollingService(IHttpClientWrapper httpClient, IBatchRepository batchRepository, IRoomRepository roomRepository, IUserRepository userRepository)
        {
            this.httpClient = httpClient;
            this.batchRepository = batchRepository;
            this.roomRepository = roomRepository;
            this.userRepository = userRepository;
        }
        /// <summary>
        ///  Poll all Service Hub databases through their respective API's and updates our database with the new data 
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
            foreach (var user in users)
            {
                UpdateUser(user);
            }
        }
        
        /// <summary>
        ///  Get batch by batchId (taken from service hubs api call)
        ///  then convert the service hub model to housing model and update our database with the new information 
        /// </summary>
        private void UpdateBatch(ApiBatch batch)
        {
            var housingBatch = batchRepository.GetBatchByBatchId(batch.BatchId);
            housingBatch = housingBatch.ConvertFromServiceModel(apiBatch: batch);
            batchRepository.UpdateBatch(housingBatch); //TODO: Fix this method call when the function gets added
        }

        /// <summary>
        ///  Get room by roomId (taken from service hubs api call)
        ///  then convert the service hub model to housing model and update our database with the new information 
        /// </summary>
        private void UpdateRoom(ApiRoom room)
        {
            var housingRoom = roomRepository.GetRoomByRoomId(room.RoomId);
            housingRoom = housingRoom.ConvertFromServiceModel(apiRoom: room);
            roomRepository.UpdateRoom(housingRoom); //TODO: Fix this method call when the function gets added
        }

        /// <summary>
        ///  Get user by userId (taken from service hubs api call)
        ///  then convert the service hub model to housing model and update our database with the new information 
        /// </summary>
        private void UpdateUser(ApiUser user)
        {
            var housingUser = userRepository.GetUserByUserId(user.UserId);
            housingUser = housingUser.ConvertFromServiceModel(apiUser: user);
            userRepository.UpdateUser(housingUser); //TODO: Fix this method call when the function gets added
        }
    }
}