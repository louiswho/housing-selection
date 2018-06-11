using Housing.Selection.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Selection.Context.Interfaces
{
    /// <summary>
    ///  Creates, reads and updates  Revature Rooms from Housing-Selection database.
    /// </summary>
    public interface IRoomRepository
    {
        IEnumerable<Room> GetRooms();
        Room GetRoomById(Guid id);
        void AddRoom(Room room);
        void SaveChanges();



    }
}
