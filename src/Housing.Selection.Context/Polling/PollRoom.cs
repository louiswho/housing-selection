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
        private IServiceRoomRetrieval roomRetrieval;

        public PollRoom(IRoomRepository roomRepository, IServiceRoomRetrieval roomRetrieval)
        {
            this.roomRepository = roomRepository;
            this.roomRetrieval = roomRetrieval;
        }
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

        public Room UpdateRoom(ApiRoom room)
        {
            var housingRoom = roomRepository.GetRoomByRoomId(room.RoomId);
            housingRoom = housingRoom.ConvertFromServiceModel(apiRoom: room);
            roomRepository.SaveChanges();
            return housingRoom;
        }
    }
}