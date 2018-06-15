using System.Collections.Generic;
using System.Threading.Tasks;
using Housing.Selection.Context.DataAccess;
using Housing.Selection.Context.HttpRequests;
using Housing.Selection.Library.HousingModels;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Context.Polling
{
    public class PollRoom : IPollRoom
    {
        private IRoomRepository roomRepository;
        private IServiceRoomCalls roomRetrieval;

        public PollRoom(IRoomRepository roomRepository, IServiceRoomCalls roomRetrieval)
        {
            this.roomRepository = roomRepository;
            this.roomRetrieval = roomRetrieval;
        }

        /// <summary>
        /// Updates the Rooms in the housing Room database based on the data retrieved from the service hub database
        /// </summary>
        /// <returns>
        /// Returns a Task<List<Room>> that contains the updated Room list
        /// </returns>
        public async Task<List<Room>> RoomPoll()
        {
            var roomList = new List<Room>();
            var rooms = await roomRetrieval.RetrieveAllRoomsAsync();
            if (rooms != null)
            {
                foreach (var room in rooms)
                {
                    roomList.Add(UpdateRoom(room));
                }
            }
            return roomList;
        }

        /// <summary>
        /// Updates a single Room in the housing Room database based on the Room data retrieved from the service hub database
        /// </summary>
        /// <param name="apiRoom">
        /// The ApiRoom object retrieved from the RoomRetireval
        /// Contains the properties to update housing's matching Room with
        /// </param>
        /// <returns>
        /// Returns a Room that contains the updated properties
        /// </returns>
        public Room UpdateRoom(ApiRoom apiRoom)
        {
            var housingRoom = roomRepository.GetRoomByRoomId(apiRoom.RoomId);
            housingRoom = housingRoom.ConvertFromServiceModel(apiRoom: apiRoom);
            roomRepository.SaveChanges();
            return housingRoom;
        }
    }
}