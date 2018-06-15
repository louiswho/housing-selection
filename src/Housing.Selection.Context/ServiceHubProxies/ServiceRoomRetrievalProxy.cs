using Housing.Selection.Library.HousingModels;
using Housing.Selection.Library.ServiceHubModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Housing.Selection.Context.ServiceHubProxies
{
   public class ServiceRoomRetrievalProxy
    {
        public List<ApiRoom> _rooms;

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
                    Gender = "Male",
                    Location = ""
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
                   City = "Tampa",
                   Country = "USA",
                   PostalCode = "33620"
               },
               Gender = "Male",
               Location = ""
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
                    Gender = "Female",
                    Location = ""
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
                        City = "Tampa",
                        Country = "USA",
                        PostalCode = "33620"
                    },
                    Gender = "Male",
                    Location = ""
                } 
                );
        }
        public async Task<List<ApiRoom>> RetrieveAllRoomsAsync()
        {
            return _rooms;
        }


    }
}
