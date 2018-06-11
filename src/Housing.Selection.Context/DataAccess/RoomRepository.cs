using Housing.Selection.Context.Interfaces;
using Housing.Selection.Library;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Housing.Selection.Context.DataAccess
{

    /// <summary>
    /// Create, read, update and get by Id Revature rooms from Housing-Selection database.
    /// </summary>
    /// 

   public class RoomRepository : IRoomRepository
    {

        private List<Room> rooms = new List<Room>();

        public RoomRepository()
        {
           

        }
        public void AddRoom(Room room)
        {
            rooms.Add(room);
        }

        public Room GetRoomById(Guid id)
        {
            return rooms.First(x => x.RoomId == id);
        }

        public IEnumerable<Room> GetRooms()
        {
            return rooms;
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
