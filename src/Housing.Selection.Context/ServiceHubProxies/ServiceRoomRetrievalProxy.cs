using Housing.Selection.Library.ServiceHubModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Housing.Selection.Context.ServiceHubProxies
{
   public class ServiceRoomRetrievalProxy
    {
       private List<ApiRoom> _rooms;

        public ServiceRoomRetrievalProxy( )
        {
            _rooms = new List<ApiRoom>();

            _rooms.Add(

                new ApiRoom()
                {
                    RoomId = Guid.NewGuid(),
                    Vacancy = 1,
                    Occupancy = 2,
                    Address = new ApiAddress()
                    {
                        AddressId = Guid.NewGuid(),
                        Address1 = "12977 N 50th St",
                        City = "Tampa",
                        Country = "USA",
                        PostalCode = "33620"
                    },
                    Gender = "M",
                    Location = "Tampa"
                }
                );
            _rooms.Add(

           new ApiRoom()
           {
               RoomId = Guid.NewGuid(),
               Vacancy = 5,
               Occupancy = 1,
               Address = new ApiAddress()
               {
                   AddressId = Guid.NewGuid(),
                   Address1 = "12977 E 6th St",
                   City = "Reston",
                   Country = "USA",
                   PostalCode = "33690"
               },
               Gender = "M",
               Location = "Reston"
           }
           );
            _rooms.Add(
                new ApiRoom()
                {
                    RoomId = Guid.NewGuid(),
                    Vacancy = 1,
                    Occupancy = 2,
                    Address = new ApiAddress()
                    {
                        AddressId = Guid.NewGuid(),
                        Address1 = "12977 N 50th St",
                        City = "Tampa",
                        Country = "USA",
                        PostalCode = "33620"
                    },
                    Gender = "F",
                    Location = "Tampa"
                }
                );
                _rooms.Add(
                new ApiRoom()
                {
                    RoomId = Guid.NewGuid(),
                    Vacancy = 1,
                    Occupancy = 2,
                    Address = new ApiAddress()
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
                );
        }
        public async Task<IEnumerable<ApiRoom>> RetrieveAllRoomsAsync()
        {
            return _rooms;
        }

    }
}
