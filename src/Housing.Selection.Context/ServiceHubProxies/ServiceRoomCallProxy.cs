using Housing.Selection.Context.HttpRequests;
using Housing.Selection.Library.ServiceHubModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Housing.Selection.Context.ServiceHubProxies
{
   public class ServiceRoomCallProxy : IServiceRoomCalls
    {
       private readonly List<ApiRoom> _rooms;

        public ServiceRoomCallProxy( )
        {
            _rooms = new List<ApiRoom>
            {
                new ApiRoom
                {
                    RoomId = Guid.NewGuid(),
                    Vacancy = 1,
                    Occupancy = 5,
                    Address = new ApiAddress
                    {
                        AddressId = Guid.NewGuid(),
                        Address1 = "12977 N 50th St",
                        City = "Tampa",
                        Country = "USA",
                        PostalCode = "33620"
                    },
                    Gender = "M",
                    Location = "Tampa"
                },
                new ApiRoom
                {
                    RoomId = Guid.NewGuid(),
                    Vacancy = 5,
                    Occupancy = 5,
                    Address = new ApiAddress
                    {
                        AddressId = Guid.NewGuid(),
                        Address1 = "12977 E 6th St",
                        City = "Reston",
                        Country = "USA",
                        PostalCode = "33690"
                    },
                    Gender = "M",
                    Location = "Reston"
                },
                new ApiRoom
                {
                    RoomId = Guid.NewGuid(),
                    Vacancy = 1,
                    Occupancy = 4,
                    Address = new ApiAddress
                    {
                        AddressId = Guid.NewGuid(),
                        Address1 = "12977 N 50th St",
                        City = "Tampa",
                        Country = "USA",
                        PostalCode = "33620"
                    },
                    Gender = "F",
                    Location = "Tampa"
                },
                new ApiRoom
                {
                    RoomId = Guid.NewGuid(),
                    Vacancy = 1,
                    Occupancy = 4,
                    Address = new ApiAddress
                    {
                        AddressId = Guid.NewGuid(),
                        Address1 = "2919 Network",
                        City = "Reston",
                        Country = "USA",
                        PostalCode = "33670"
                    },
                    Gender = "M",
                    Location = "Reston"
                }
            };

        }

        public async Task UpdateRoomAsync(ApiRoom room)
        {
            if(room.RoomId == Guid.Empty ) throw new Exception("Update failed for room with RoomId " + room.RoomId);
            
            var _room = _rooms.First(x => x.RoomId == room.RoomId);
            if (room == null) throw new Exception("Update failed for room with RoomId " + room.RoomId);

            _room.Vacancy = room.Vacancy;

        }

        public async Task<List<ApiRoom>> RetrieveAllRoomsAsync()
        {
            return _rooms;
        }
    }
}
