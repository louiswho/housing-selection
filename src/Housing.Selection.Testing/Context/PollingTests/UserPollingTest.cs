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
    public class UserPollingTest
    {
        private User user1, user2;
        private ApiUser apiUser1, apiUser2;
        private Room room1;
        private Batch batch1;

        private List<User> mockUserList;
        private Task<List<ApiUser>> mockTaskApiUserList;
        private Task<List<ApiRoom>> mockTaskApiRoomList;
        private Task<List<ApiBatch>> mockTaskApiBatchList;

        private List<ApiUser> mockApiUserList;
        private List<ApiRoom> mockApiRoomList;
        private List<ApiBatch> mockApiBatchList;

        private PollUser pollUser;

        public UserPollingTest()
        {
            PollingSetupUsers();
            PollingSetupRooms();
            PollingSetupBatch();

            var mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(x => x.GetUserByUserId(It.IsAny<Guid>())).Returns(user1);
            var mockUserRetrieval = new Mock<IServiceUserCalls>();
            mockUserRetrieval.Setup(x => x.RetrieveAllUsersAsync()).Returns(mockTaskApiUserList);
            var mockRoomRepo = new Mock<IRoomRepository>();
            mockRoomRepo.Setup(x => x.GetRoomByRoomId(It.IsAny<Guid>())).Returns(room1);
            var mockRoomRetrieval = new Mock<IServiceRoomCalls>();
            mockRoomRetrieval.Setup(x => x.RetrieveAllRoomsAsync()).Returns(mockTaskApiRoomList);
            var mockBatchRepo = new Mock<IBatchRepository>();
            mockBatchRepo.Setup(x => x.GetBatchByBatchId(It.IsAny<Guid>())).Returns(batch1);
            var mockBatchRetrieval = new Mock<IServiceBatchCalls>();
            mockBatchRetrieval.Setup(x => x.RetrieveAllBatchesAsync()).Returns(mockTaskApiBatchList);
            var mockAddressRepo = new Mock<IAddressRepository>();
            mockAddressRepo.Setup(x => x.GetAddressByAddressId(It.IsAny<Guid>())).Returns(room1.Address);
            var mockNameRepo = new Mock<INameRepository>();
            mockNameRepo.Setup(x => x.GetNameByNameId(It.IsAny<Guid>())).Returns(user1.Name);            

            pollUser = new PollUser(mockUserRepo.Object, mockUserRetrieval.Object, mockAddressRepo.Object,
                                    mockNameRepo.Object, mockBatchRepo.Object, mockBatchRetrieval.Object, 
                                    mockRoomRepo.Object, mockRoomRetrieval.Object);
        }

        [Fact]
        public async void Test_User_Poll()
        {
            mockUserList.Add(user1);
            mockUserList.Add(user1);
            var expected = mockUserList;
            var result = await pollUser.UserPoll();

            Assert.Equal(expected, result);
        }

        [Fact]
        public async void Test_User_Poll_Fail()
        {
            mockUserList.Add(user1);
            mockUserList.Add(user2);            
            var expected = mockUserList;
            var result = await pollUser.UserPoll();

            Assert.NotEqual(expected, result);
        }
        [Fact]
        public void Test_User_Update()
        {
            var expected = user1;
            var result = pollUser.UpdateUser(apiUser1, mockApiBatchList, mockApiRoomList);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_User_Update_Fail()
        {
            var expected = user2;
            var result = pollUser.UpdateUser(apiUser1, mockApiBatchList, mockApiRoomList);

            Assert.NotEqual(expected, result);
        }

        [Fact]
        public void Test_GetBatchId()
        {            
            var expected = batch1;
            var result = pollUser.GetBatchId(apiUser1, mockApiBatchList);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_GetBatchId_Fail()
        {
            Batch batch2 = new Batch()
            {
                Id = Guid.NewGuid(),
                BatchId = Guid.NewGuid(),
                StartDate = DateTime.Today,
                EndDate = DateTime.Today,
                BatchName = "Batch Two",
                BatchOccupancy = 2,
                BatchSkill = "None",
                Location = "USF"
            };
            var expected = batch2;
            var result = pollUser.GetBatchId(apiUser1, mockApiBatchList);

            Assert.NotEqual(expected, result);
        }

        [Fact]
        public void Test_GetRoomId()
        {
            var expected = room1;
            var result = pollUser.GetRoomId(apiUser1, mockApiRoomList);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_GetRoomId_Fail()
        {
            Room room2 = new Room()
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
            var expected = room2;
            var result = pollUser.GetRoomId(apiUser1, mockApiRoomList);

            Assert.NotEqual(expected, result);
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
            user2 = new User()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Location = "222",
                Email = "user2@user2.com",
                Type = "User2",
                Gender = "M",
                Address = new Address()
                {
                    Id = Guid.NewGuid(),
                    AddressId = Guid.NewGuid(),
                    Address2 = "222 Room2 St",
                    City = "Tampa",
                    State = "FL",
                    PostalCode = "22222",
                    Country = "US"
                },
                Name = new Name()
                {
                    Id = Guid.NewGuid(),
                    NameId = Guid.NewGuid(),
                    First = "John2",
                    Middle = "Doe",
                    Last = "Doe2"
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
            apiUser2 = new ApiUser()
            {
                UserId = Guid.NewGuid(),
                Location = "222",
                Email = "apiuser2@apiuser2.com",
                Type = "ApiUser2",
                Gender = 'M',
                Address = new ApiAddress()
                {
                    AddressId = Guid.NewGuid(),
                    Address2 = "222 Room2 St",
                    City = "Tampa",
                    State = "FL",
                    PostalCode = "22222",
                    Country = "US"
                },
                Name = new ApiName()
                {
                    NameId = Guid.NewGuid(),
                    First = "Api2",
                    Middle = "ApiMiddle2",
                    Last = "ApiLast2"
                }
            };

            mockUserList = new List<User>();
            mockApiUserList = new List<ApiUser>()
            {
                apiUser1,
                apiUser2
            };
            mockTaskApiUserList = Task.FromResult<List<ApiUser>>(mockApiUserList);

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

            ApiRoom apiRoom1 = new ApiRoom()
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
            ApiRoom apiRoom2 = new ApiRoom()
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
            mockApiRoomList = new List<ApiRoom>();
            mockApiRoomList.Add(apiRoom1);
            mockApiRoomList.Add(apiRoom2);
            mockTaskApiRoomList = Task.FromResult<List<ApiRoom>>(mockApiRoomList);
        }

        private void PollingSetupBatch()
        {
            List<Guid> apiUserIdList = new List<Guid>();
            foreach (var user in mockApiUserList)
            {
                apiUserIdList.Add(user.UserId);
            }

            batch1 = new Batch()
            {
                Id = Guid.NewGuid(),
                BatchId = Guid.NewGuid(),
                StartDate = DateTime.Today,
                EndDate = DateTime.Today,
                BatchName = "Batch One",
                BatchOccupancy = 1,
                BatchSkill = "None",
                Location = "Virginia"               
            };

            ApiBatch apiBatch1 = new ApiBatch()
            {
                BatchId = Guid.NewGuid(),
                StartDate = DateTime.Today,
                EndDate = DateTime.Today,
                BatchName = "Batch One",
                BatchOccupancy = 1,
                BatchSkill = "None",
                Location = "Tampa",
                UserIds = apiUserIdList
            };
            ApiBatch apiBatch2 = new ApiBatch()
            {
                BatchId = Guid.NewGuid(),
                StartDate = DateTime.Today,
                EndDate = DateTime.Today,
                BatchName = "Batch Two",
                BatchOccupancy = 2,
                BatchSkill = "None",
                Location = "Reston",
                UserIds = apiUserIdList
            };
                       
            mockApiBatchList = new List<ApiBatch>();
            mockApiBatchList.Add(apiBatch1);
            mockApiBatchList.Add(apiBatch2);
            mockTaskApiBatchList = Task.FromResult<List<ApiBatch>>(mockApiBatchList);
        }
    }
}