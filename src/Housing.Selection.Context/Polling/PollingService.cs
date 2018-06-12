using Housing.Selection.Context.HttpRequests;
using Housing.Selection.Context.DataAccess;
using Housing.Selection.Library.ServiceHubModels;
using Housing.Selection.Library.HousingModels;

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
        private IServiceBatchRetrieval batchRetrieval;
        private IServiceRoomRetrieval roomRetrieval;
        private IServiceUserRetrieval userRetrieval;
        public PollingService(IBatchRepository batchRepository, IRoomRepository roomRepository, IUserRepository userRepository, IServiceBatchRetrieval batchRetrieval, IServiceRoomRetrieval roomRetrieval, IServiceUserRetrieval userRetrieval)
        {
            this.batchRepository = batchRepository;
            this.roomRepository = roomRepository;
            this.userRepository = userRepository;
            this.batchRetrieval = batchRetrieval;
            this.roomRetrieval = roomRetrieval;
            this.userRetrieval = userRetrieval;
        }
        /// <summary>
        ///  Poll all Service Hub databases through their respective API's and updates our database with the new data 
        /// </summary>
        public async void PollAll()
        {
            var batches = await batchRetrieval.RetrieveAllBatchesAsync();
            if (batches != null)
            {
                foreach (var batch in batches)
                {
                    UpdateBatch(batch);
                }
            }
            var rooms = await roomRetrieval.RetrieveAllRoomsAsync();
            if (rooms != null)
            {
                foreach (var room in rooms)
                {
                    UpdateRoom(room);
                }
            }
            var users = await userRetrieval.RetrieveAllUsersAsync();
            if (users != null)
            {
                foreach (var user in users)
                {
                    UpdateUser(user);
                }
            }
        }

        /// <summary>
        ///  Get batch by batchId (taken from service hubs api call)
        ///  then convert the service hub model to housing model and update our database with the new information 
        /// </summary>
        public Batch UpdateBatch(ApiBatch batch)
        {
            var housingBatch = batchRepository.GetBatchByBatchId(batch.BatchId);
            housingBatch = housingBatch.ConvertFromServiceModel(apiBatch: batch);
            batchRepository.SaveChanges();
            return housingBatch;
        }

        /// <summary>
        ///  Get room by roomId (taken from service hubs api call)
        ///  then convert the service hub model to housing model and update our database with the new information 
        /// </summary>
        public Room UpdateRoom(ApiRoom room)
        {
            var housingRoom = roomRepository.GetRoomByRoomId(room.RoomId);
            housingRoom = housingRoom.ConvertFromServiceModel(apiRoom: room);
            roomRepository.SaveChanges();
            return housingRoom;
        }

        /// <summary>
        ///  Get user by userId (taken from service hubs api call)
        ///  then convert the service hub model to housing model and update our database with the new information 
        /// </summary>
        public User UpdateUser(ApiUser user)
        {
            var housingUser = userRepository.GetUserByUserId(user.UserId);
            housingUser = housingUser.ConvertFromServiceModel(apiUser: user);
            userRepository.SaveChanges();
            return housingUser;
        }
    }
}