using Housing.Selection.Library.ServiceHubModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Selection.Context.ServiceHubProxies
{
   public class ServiceRoomRetrieval
    {

        public List <ApiRoom> ProxyApiRooms()
        {
            var apiAddress = new ApiAddress() {
                AddressId = Guid.NewGuid(),
                Address1 = "12977 N 50th St",
                City = "Tampa",
                Country = "USA",
                PostalCode = "33620",
            };

            var apiAddress2 = new ApiAddress()
            {
                AddressId = Guid.NewGuid(),
                Address1 = "2919 Network",
                City = "Tampa",
                Country = "USA",
                PostalCode = "33620",
            };

            var apiRoom1 = new ApiRoom()
            {
                RoomId = Guid.NewGuid(),
                Vacancy = 1,
                Occupancy = 2,
                Address = apiAddress,
                Gender = "Male",
                Location = ""               
            };
            var apiRoom2 = new ApiRoom()
            {
                RoomId = Guid.NewGuid(),
                Vacancy = 2,
                Occupancy = 2,
                Address = apiAddress,
                Gender = "Male",
                Location = "Reston"
            };
            var apiRoom3 = new ApiRoom()
            {
                RoomId = Guid.NewGuid(),
                Vacancy = 2,
                Occupancy = 3,
                Address = apiAddress,
                Gender = "Female",
                Location = "Tampa"
            };
            var apiRoom4 = new ApiRoom()
            {
                RoomId = Guid.NewGuid(),
                Vacancy = 2,
                Occupancy = 3,
                Address = apiAddress,
                Gender = "Female",
                Location = "Tampa"
            };
            var apiRooms = new List<ApiRoom>() {
                apiRoom1,
                apiRoom2,
                apiRoom3
            }; 
            
            return apiRooms;           
        }
             
    }
}
