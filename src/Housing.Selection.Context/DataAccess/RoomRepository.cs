using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Housing.Selection.Library.HousingModels;
using Microsoft.EntityFrameworkCore;

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
            _housingSelectionDbContext.SaveChanges();
        }

        public async Task<Room> GetRoomById(Guid id)
        {
            return await _housingSelectionDbContext.Rooms
                .Include(x => x.Address)
                .Include(y => y.Users)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Room> GetRoomByRoomId(Guid roomId)
        {
            return await _housingSelectionDbContext.Rooms
                .Include(x => x.Address)
                .Include(y => y.Users)
                .FirstOrDefaultAsync(x => x.RoomId == roomId);
        }

        public IEnumerable<Room> GetRooms()
        {
            return _housingSelectionDbContext.Rooms
                .Include(x => x.Address)
                .Include(y => y.Users);
        }

        public async Task SaveChangesAsync()
        {
            await _housingSelectionDbContext.SaveChangesAsync();
        }
    }
}
