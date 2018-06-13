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

namespace Housing.Selection.Testing.Context
{
    public class PollingTest
    {

        private Room room1, room2, room3;
        private ApiRoom apiRoom1;
        private User user1, user2, user3;
        private ApiUser apiUser1;
        private Batch batch1, batch2, batch3;
        private ApiBatch apiBatch1, apiBatch2;

        private List<Room> mockRoomList;
        private Task<List<ApiBatch>> mockApiBatchList;
        private Task<List<ApiRoom>> mockApiRoomList;
        private Task<List<ApiUser>> mockApiUserList;

        private PollingService pollingService;
        private Mock<IUserRepository> mockUserRepo;
        private Mock<IBatchRepository> mockBatchRepo;

        public PollingTest()
        {
            PollingSetup();

            mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(x => x.GetUserByUserId(It.IsAny<Guid>())).Returns(user1);
            var mockRoomRepo = new Mock<IRoomRepository>();
            mockRoomRepo.Setup(x => x.GetRoomByRoomId(It.IsAny<Guid>())).Returns(room1);
            mockBatchRepo = new Mock<IBatchRepository>();
            mockBatchRepo.Setup(x => x.GetBatchByBatchId(It.IsAny<Guid>())).Returns(batch1);
            

            var mockBatchRetrieval = new Mock<IServiceBatchRetrieval>();
            mockBatchRetrieval.Setup(x => x.RetrieveAllBatchesAsync()).Returns(mockApiBatchList);
            var mockRoomRetrieval = new Mock<IServiceRoomRetrieval>();
            mockRoomRetrieval.Setup(x => x.RetrieveAllRoomsAsync()).Returns(mockApiRoomList);
            var mockUserRetrieval = new Mock<IServiceUserRetrieval>();
            mockUserRetrieval.Setup(x => x.RetrieveAllUsersAsync()).Returns(mockApiUserList);

            pollingService = new PollingService(mockBatchRepo.Object, mockRoomRepo.Object, mockUserRepo.Object, mockBatchRetrieval.Object, mockRoomRetrieval.Object, mockUserRetrieval.Object);
        }

        [Fact]
        public void Test_Batch_Update()
        {
            var expected = batch1;
            var result = pollingService.UpdateBatch(apiBatch1);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Batch_Update_Fail()
        {
            var expected = batch2;
            var result = pollingService.UpdateBatch(apiBatch1);

            Assert.NotEqual(expected, result);
        }

        [Fact]
        public void Test_Room_Update()
        {
            var expected = room1;
            var result = pollingService.UpdateRoom(apiRoom1);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Room_Update_Fail()
        {
            var expected = room2;
            var result = pollingService.UpdateRoom(apiRoom1);

            Assert.Equal(expected, result);
        }
        [Fact]
        public void Test_User_Update()
        {
            var expected = user1;
            var result = pollingService.UpdateUser(apiUser1);
            
            Assert.Equal(expected, result);
        }
        private void PollingSetup()
        {
            PollingSetupBatch();
            PollingSetupRooms();
            PollingSetupUsers();
        }
        private void PollingSetupBatch()
        {
            batch1 = new Batch()
            {
                Id = Guid.NewGuid(),
                BatchId = Guid.NewGuid(),
                StartDate = DateTime.Today,
                EndDate = DateTime.Today,
                BatchName = "Batch One",
                BatchOccupancy = 1,
                BatchSkill = "None",
                Address = new Address()
                {
                    Id = Guid.NewGuid(),
                    AddressId = Guid.NewGuid(),
                    Address1 = "111 Batch1 St",
                    City = "Tampa",
                    State = "FL",
                    PostalCode = "11111",
                    Country = "US"
                }
            };
            apiBatch1 = new ApiBatch()
            {
                BatchId = Guid.NewGuid(),
                StartDate = DateTime.Today,
                EndDate = DateTime.Today,
                BatchName = "Batch One",
                BatchOccupancy = 1,
                BatchSkill = "None",
                Address = new ApiAddress()
                {
                    AddressId = Guid.NewGuid(),
                    Address1 = "111 Batch1 St",
                    City = "Tampa",
                    State = "FL",
                    PostalCode = "11111",
                    Country = "US"
                }
            };
            apiBatch2 = new ApiBatch()
            {
                BatchId = Guid.NewGuid(),
                StartDate = DateTime.Today,
                EndDate = DateTime.Today,
                BatchName = "Batch Two",
                BatchOccupancy = 2,
                BatchSkill = "None",
                Address = new ApiAddress()
                {
                    AddressId = Guid.NewGuid(),
                    Address1 = "222 Batch2 St",
                    City = "Tampa",
                    State = "FL",
                    PostalCode = "22222",
                    Country = "US"
                }
            };
            List<ApiBatch> apiBatchList = new List<ApiBatch>();
            apiBatchList.Add(apiBatch1);
            apiBatchList.Add(apiBatch2);
            mockApiBatchList = Task.FromResult<List<ApiBatch>>(apiBatchList);


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
            mockRoomList.Add(room1);
            mockRoomList.Add(room2);
            mockRoomList.Add(room3);

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
        }
        private void PollingSetupUsers()
        {            
            user1 = new User()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Location = "111",
                Email = "user1@user1.com",
                Type = "User1",
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
                },
                Name = new Name()
                {
                    Id = Guid.NewGuid(),
                    NameId = Guid.NewGuid(),
                    First = "John",
                    Middle = "Doe",
                    Last = "Doe"                   
                }
                
            };
            apiUser1 = new ApiUser()
            {                
                UserId = Guid.NewGuid(),
                Location = "111",
                Email = "apiuser1@apiuser1.com",
                Type = "ApiUser1",
                Gender = 'M',
                Address = new ApiAddress()
                {
                    AddressId = Guid.NewGuid(),
                    Address1 = "111 Room1 St",
                    City = "Tampa",
                    State = "FL",
                    PostalCode = "11111",
                    Country = "US"
                },
                Name = new ApiName()
                {
                    NameId = Guid.NewGuid(),
                    First = "Api",
                    Middle = "ApiMiddle",
                    Last = "ApiLast"
                }
            };
        }
    }
}