using System.Collections.Generic;
using System.Threading.Tasks;
using Housing.Selection.Library.HousingModels;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Context.Polling
{
    public interface IPollRoom
    {
        Task<List<Room>> RoomPoll();
        Task<Room> UpdateRoom(ApiRoom room);
    }
}