using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Housing.Selection.Library.HousingModels;

namespace Housing.Selection.Context.DataAccess
{
    public interface IRoomRepository
    {
        IEnumerable<Room> GetRooms();
        Task<Room> GetRoomById(Guid id);
        Task<Room> GetRoomByRoomId(Guid roomId);
        void AddRoom(Room room);
        Task SaveChangesAsync();
    }
}
