using Housing.Selection.Library;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Housing.Selection.Library.HousingModels;

namespace Housing.Selection.Context.DataAccess
{

    /// <summary>
    /// Create, read, update and get by Id Revature rooms from Housing-Selection database.
    /// </summary>
    /// 

   public class RoomRepository : IRoomRepository
    {

        private readonly IDbContext _HousingSelectionDbContext;

        private List<User> users = new List<User>();

        public RoomRepository(IDbContext housingSelectionContext)
        {
            _HousingSelectionDbContext = housingSelectionContext;
        }


        public RoomRepository()
        {
           

        }

        // Add room to Housing-Selection database 
        public void AddRoom(Room room)
        {
            _HousingSelectionDbContext.Rooms.Add(room);
        }

        public Room GetRoomById(Guid id)
        {
            return _HousingSelectionDbContext.Rooms.First(x => x.Id == id);
        }

        public Room GetRoomByRoomId(Guid roomId)
        {
            return  _HousingSelectionDbContext.Rooms.First(x => x.RoomId == roomId);
        }

        public IEnumerable<Room> GetRooms()
        {
            return _HousingSelectionDbContext.Rooms;
        }

        public int SaveChanges()
        {
          return _HousingSelectionDbContext.saveChanges();
        }
    }
}
