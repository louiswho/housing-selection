using System;
using System.Collections.Generic;
using Housing.Selection.Library.HousingModels;

namespace Housing.Selection.Context.DataAccess
{
    public interface IRoomRepository
    {
        IEnumerable<Room> GetRooms();
        Room GetRoomById(Guid id);
        Room GetRoomByRoomId(Guid roomId);
        void AddRoom(Room room);
        void SaveChanges();
    }
}
