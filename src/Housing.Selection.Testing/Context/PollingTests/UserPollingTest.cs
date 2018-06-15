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
        private List<User> mockUserList;
        private Task<List<ApiUser>> mockTaskApiUserList;
        private Task<List<ApiRoom>> mockTaskApiRoomList;
        private Task<List<ApiBatch>> mockTaskApiBatchList;
        private List<ApiUser> mockApiUserList;
        private List<ApiRoom> mockApiRoomList;
        private List<ApiBatch> mockApiBatchList;
        private Mock<IUserRepository> mockUserRepo;

        private Mock<IRoomRepository> mockRoomRepo;
        private Mock<IServiceRoomRetrieval> mockRoomRetrieval;

        private Mock<IBatchRepository> mockBatchRepo;
        private Mock<IAddressRepository> mockAddressRepo;
        private Mock<INameRepository> mockNameRepo;
        private PollUser pollUser;
        public UserPollingTest()
        {
            PollingSetupUsers();

            mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(x => x.GetUserByUserId(It.IsAny<Guid>())).Returns(user1);
            var mockUserRetrieval = new Mock<IServiceUserRetrieval>();
            mockUserRetrieval.Setup(x => x.RetrieveAllUsersAsync()).Returns(mockTaskApiUserList);
            var mockRoomRetrieval = new Mock<IServiceRoomRetrieval>();
            mockRoomRetrieval.Setup(x => x.RetrieveAllRoomsAsync()).Returns(mockTaskApiRoomList);
            var mockBatchRetrieval = new Mock<IServiceBatchRetrieval>();
            mockBatchRetrieval.Setup(x => x.RetrieveAllBatchesAsync()).Returns(mockTaskApiBatchList);            

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
            var apiUserList = new List<ApiUser>()
            {
                apiUser1,
                apiUser2
            };
            mockTaskApiUserList = Task.FromResult<List<ApiUser>>(apiUserList);

        }
    }
}