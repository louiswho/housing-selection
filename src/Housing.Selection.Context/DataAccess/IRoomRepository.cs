using Housing.Selection.Library;
using System;
using System.Collections.Generic;
using System.Text;
using Housing.Selection.Library.HousingModels;

namespace Housing.Selection.Context.DataAccess
{
    /// <summary>
    ///  Creates, reads and updates  Revature Rooms from Housing-Selection database.
    /// </summary>
    public interface IRoomRepository
    {
        IEnumerable<Room> GetRooms();
        Room GetRoomById(Guid id);
        Room GetRoomByRoomId(Guid roomId);
        void AddRoom(Room room);
        int SaveChanges();
    }
}
