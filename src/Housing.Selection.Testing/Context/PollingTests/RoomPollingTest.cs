using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Housing.Selection.Context.DataAccess;
using Housing.Selection.Context.HttpRequests;
using Housing.Selection.Context.Polling;
using Housing.Selection.Library.HousingModels;
using Housing.Selection.Library.ServiceHubModels;
using Moq;
using Xunit;

namespace Housing.Selection.Testing.Context.PollingTests
{
    public class RoomPollingTest
    {
        private Room room1, room2, room3;
        private ApiRoom apiRoom1, apiRoom2;
        private List<Room> mockRoomList;
        private Task<List<ApiRoom>> mockApiRoomList;
        private Mock<IRoomRepository> mockRoomRepo;

        private PollRoom pollRoom;
        public RoomPollingTest()
        {
            PollingSetupRooms();
            mockRoomRepo = new Mock<IRoomRepository>();
            mockRoomRepo.Setup(x => x.GetRoomByRoomId(It.IsAny<Guid>())).Returns(Task.FromResult<Room>(room1));
            var mockRoomRetrieval = new Mock<IServiceRoomCalls>();
            mockRoomRetrieval.Setup(x => x.RetrieveAllRoomsAsync()).Returns(mockApiRoomList);

            pollRoom = new PollRoom(mockRoomRepo.Object, mockRoomRetrieval.Object);
        }
        [Fact]
        public async void Test_Room_Poll()
        {
            var expected = mockRoomList;
            var result = await pollRoom.RoomPoll();

            Assert.Equal(expected, result);
        }

        [Fact]
        public async void Test_Room_Poll_Fail()
        {
            mockRoomList.Add(room2);
            var expected = mockRoomList;
            var result = await pollRoom.RoomPoll();

            Assert.NotEqual(expected, result);
        }

        [Fact]
        public void Test_Room_Update()
        {
            var expected = room1;
            var result = pollRoom.UpdateRoom(apiRoom1).Result;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Room_Update_Fail()
        {
            var expected = room2;
            var result = pollRoom.UpdateRoom(apiRoom1).Result;

            Assert.NotEqual(expected, result);
        }

        private void PollingSetupRooms()
        {
            room1 = new Room()
            {
                Id = Guid.NewGuid(),
                RoomId = Guid.NewGuid(),
                Location = "111",
                Vacancy = 1,
                Occupancy = 3,
                Gender = "M",
                Address = new Address()
                {
                    Id = Guid.NewGuid(),
                    AddressId = Guid.NewGuid(),
                    Address1 = "111 Room1 St",
                    City = "Tampa",
                    State = "FL",
                    PostalCode = "11111",
                    Country = "US"
                }
            };
            room2 = new Room()
            {
                Id = Guid.NewGuid(),
                RoomId = Guid.NewGuid(),
                Location = "222",
                Vacancy = 2,
                Occupancy = 3,
                Gender = "M",
                Address = new Address()
                {
                    Id = Guid.NewGuid(),
                    AddressId = Guid.NewGuid(),
                    Address1 = "222 Room 2 St",
                    City = "Tampa",
                    State = "FL",
                    PostalCode = "22222",
                    Country = "US"
                }
            };
            room3 = new Room()
            {
                Id = Guid.NewGuid(),
                RoomId = Guid.NewGuid(),
                Location = "333",
                Vacancy = 3,
                Occupancy = 3,
                Gender = "M",
                Address = new Address()
                {
                    Id = Guid.NewGuid(),
                    AddressId = Guid.NewGuid(),
                    Address1 = "333 Room 3 St",
                    City = "Tampa",
                    State = "FL",
                    PostalCode = "33333",
                    Country = "US"
                }
            };
            mockRoomList = new List<Room>();
            mockRoomList.Add(room1); //Moq returns room1 when GetRoomByRoomId is called, so we expect to get a list of
            mockRoomList.Add(room1); //size apiRoomList containing multiple room1's

            apiRoom1 = new ApiRoom()
            {
                RoomId = Guid.NewGuid(),
                Location = "111 API",
                Vacancy = 1,
                Occupancy = 3,
                Gender = "M",
                Address = new ApiAddress()
                {
                    AddressId = Guid.NewGuid(),
                    Address1 = "111 Api Room 1 St",
                    City = "Tampa",
                    State = "FL",
                    PostalCode = "11111",
                    Country = "US"
                }
            };
            apiRoom2 = new ApiRoom()
            {
                RoomId = Guid.NewGuid(),
                Location = "222",
                Vacancy = 2,
                Occupancy = 3,
                Gender = "M",
                Address = new ApiAddress()
                {
                    AddressId = Guid.NewGuid(),
                    Address1 = "222 ApiRoom 2 St",
                    City = "Tampa",
                    State = "FL",
                    PostalCode = "22222",
                    Country = "US"
                }
            };
            List<ApiRoom> apiRoomList = new List<ApiRoom>();
            apiRoomList.Add(apiRoom1);
            apiRoomList.Add(apiRoom2);
            mockApiRoomList = Task.FromResult<List<ApiRoom>>(apiRoomList);
        }
    }
}