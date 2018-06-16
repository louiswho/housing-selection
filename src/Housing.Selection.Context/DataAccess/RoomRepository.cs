using System;
using System.Collections.Generic;
using System.Linq;
using Housing.Selection.Library.HousingModels;

namespace Housing.Selection.Context.DataAccess
{
   public class RoomRepository : IRoomRepository
    {
        private readonly IDbContext _housingSelectionDbContext;

        public RoomRepository(IDbContext housingSelectionContext)
        {
            _housingSelectionDbContext = housingSelectionContext;
        }

        public void AddRoom(Room room)
        {
            _housingSelectionDbContext.Rooms.Add(room);
        }

        public Room GetRoomById(Guid id)
        {
            return _housingSelectionDbContext.Rooms.First(x => x.Id == id);
        }

        public Room GetRoomByRoomId(Guid roomId)
        {
            return  _housingSelectionDbContext.Rooms.First(x => x.RoomId == roomId);
        }

        public IEnumerable<Room> GetRooms()
        {
            return _housingSelectionDbContext.Rooms;
        }
 
        public void SaveChanges()
        {
            _housingSelectionDbContext.SaveChanges();
        }
    }
}
