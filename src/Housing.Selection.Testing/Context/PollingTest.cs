using System;
using System.Collections.Generic;
using Housing.Selection.Context.DataAccess;
using Housing.Selection.Library.HousingModels;
using Moq;

namespace Housing.Selection.Testing.Context
{
    public class PollingTest
    {

        private Room room1, room2, room3;
        private User user1, user2, user3;
        private Batch batch1, batch2, batch3;

        public List<Room> mockRoomList;


        public PollingTest()
        {     
            PollingSetup();       
            var mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(x => x.GetUserByUserId(It.IsAny<Guid>())).Returns(user1);    
            var mockRoomRepo = new Mock<IRoomRepository>();
            mockRoomRepo.Setup(x => x.GetRoomByRoomId(It.IsAny<Guid>())).Returns(room1);
            var mockBatchRepo = new Mock<IBatchRepository>();

        }
        private void PollingSetup()
        {
            PollingSetupBatch();
            PollingSetupRooms();

        }
        private void PollingSetupBatch()
        {

        }
        private void PollingSetupRooms()
        {
            room1 = new Room()
            {
                Id = new Guid("id1"),
                RoomId = new Guid("roomId1"),
                Location = "111",
                Vacancy = 1,
                Occupancy = 3,
                Gender = 'M',
                Address = new Address()
                {
                    Id = new Guid("aId1"),
                    AddressId = new Guid("addressId1"),
                    Address1 = "111 Room1 St",
                    City = "Tampa",
                    State = "FL",
                    PostalCode = "33333",
                    Country = "US"
                }
            };
            room2 = new Room()
            {
                Id = new Guid("id2"),
                RoomId = new Guid("roomId2"),
                Location = "222",
                Vacancy = 2,
                Occupancy = 3,
                Gender = 'M',
                Address = new Address()
                {
                    Id = new Guid("aId2"),
                    AddressId = new Guid("addressId2"),
                    Address1 = "222 Room 2 St",
                    City = "Tampa",
                    State = "FL",
                    PostalCode = "22222",
                    Country = "US"
                }
            };
            room3 = new Room()
            {
                Id = new Guid("id3"),
                RoomId = new Guid("roomId3"),
                Location = "333",
                Vacancy = 3,
                Occupancy = 3,
                Gender = 'M',
                Address = new Address()
                {
                    Id = new Guid("aId3"),
                    AddressId = new Guid("addressId3"),
                    Address1 = "333 Room 3 St",
                    City = "Tampa",
                    State = "FL",
                    PostalCode = "33333",
                    Country = "US"
                }
            };
            mockRoomList.Add(room1);
            mockRoomList.Add(room2);
            mockRoomList.Add(room3);
        }
    }
}