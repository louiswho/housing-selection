using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using Housing.Selection.Context.HttpRequests;
using System.Threading.Tasks;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Testing.Context
{
    public class HttpRequestsTests
    {
        [Fact]
        public async Task GetAllBatches_StatusCodeSuccessBatchReturned_ReturnsApiBatches()
        {
            List<ApiBatch> apibatchesexpected = new List<ApiBatch>();
            apibatchesexpected.Add(new ApiBatch());

            var response = new HttpResponseMessage();
            response.Content = new ObjectContent<List<ApiBatch>>(apibatchesexpected, new JsonMediaTypeFormatter(), "application/json");

            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(x => x.GetAsync(It.IsAny<String>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetBatchServicePath()).Returns("string");

            ServiceBatchCalls sbr = new ServiceBatchCalls(mockHttpClientWrapper.Object, mockApiPath.Object);

            var actual = await sbr.RetrieveAllBatchesAsync();

            Assert.Equal(apibatchesexpected, actual);
            Assert.Single(actual);
        }

        [Fact]
        public async Task GetAllBatches_StatusCodeSuccessNoBatches_ReturnsNull()
        {
            List<ApiBatch> apibatchesexpected = new List<ApiBatch>();

            var response = new HttpResponseMessage();
            response.Content = new ObjectContent<List<ApiBatch>>(apibatchesexpected, new JsonMediaTypeFormatter(), "application/json");

            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(x => x.GetAsync(It.IsAny<String>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetBatchServicePath()).Returns("string");

            ServiceBatchCalls sbr = new ServiceBatchCalls(mockHttpClientWrapper.Object, mockApiPath.Object);

            var actual = await sbr.RetrieveAllBatchesAsync();

            Assert.Null(actual);
        }

        [Fact]
        public async Task GetAllBatches_StatusCodeFail_ReturnsNull()
        {
            List<ApiBatch> apibatchesexpected = new List<ApiBatch>();
            apibatchesexpected.Add(new ApiBatch());

            var response = new HttpResponseMessage();
            response.Content = new ObjectContent<List<ApiBatch>>(apibatchesexpected, new JsonMediaTypeFormatter(), "application/json");
            response.StatusCode = HttpStatusCode.NotFound;

            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(x => x.GetAsync(It.IsAny<String>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetBatchServicePath()).Returns("string");

            ServiceBatchCalls sbr = new ServiceBatchCalls(mockHttpClientWrapper.Object, mockApiPath.Object);

            var actual = await sbr.RetrieveAllBatchesAsync();

            Assert.Null(actual);
        }

        [Fact]
        public async Task GetAllRooms_StatusCodeSuccessReturnsRooms_ReturnsApiRooms()
        {
            List<ApiRoom> apiroomsexpected = new List<ApiRoom>();
            apiroomsexpected.Add(new ApiRoom());

            var response = new HttpResponseMessage();
            response.Content = new ObjectContent<List<ApiRoom>>(apiroomsexpected, new JsonMediaTypeFormatter(), "application/json");

            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(x => x.GetAsync(It.IsAny<String>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetRoomServicePath()).Returns("string");

            ServiceRoomCalls sbr = new ServiceRoomCalls(mockHttpClientWrapper.Object, mockApiPath.Object);

            var actual = await sbr.RetrieveAllRoomsAsync();

            Assert.Equal(apiroomsexpected, actual);
            Assert.Single(actual);
        }

        [Fact]
        public async Task GetAllRooms_StatusCodeSuccessNoRooms_ReturnsNull()
        {
            List<ApiRoom> apiroomsexpected = new List<ApiRoom>();

            var response = new HttpResponseMessage();
            response.Content = new ObjectContent<List<ApiRoom>>(apiroomsexpected, new JsonMediaTypeFormatter(), "application/json");

            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(x => x.GetAsync(It.IsAny<String>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetRoomServicePath()).Returns("string");

            ServiceRoomCalls srr = new ServiceRoomCalls(mockHttpClientWrapper.Object, mockApiPath.Object);

            var actual = await srr.RetrieveAllRoomsAsync();

            Assert.Null(actual);
        }

        [Fact]
        public async Task GetAllRooms_StatusCodeFail_ReturnsNull()
        {
            List<ApiRoom> apiroomsexpected = new List<ApiRoom>();
            apiroomsexpected.Add(new ApiRoom());

            var response = new HttpResponseMessage();
            response.Content = new ObjectContent<List<ApiRoom>>(apiroomsexpected, new JsonMediaTypeFormatter(), "application/json");
            response.StatusCode = HttpStatusCode.NotFound;

            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(x => x.GetAsync(It.IsAny<String>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetRoomServicePath()).Returns("string");

            ServiceRoomCalls srr = new ServiceRoomCalls(mockHttpClientWrapper.Object, mockApiPath.Object);

            var actual = await srr.RetrieveAllRoomsAsync();

            Assert.Null(actual);
        }

        [Fact]
        public async Task UpdateRoomAsync_UpdateRoomValid_PutIsCalledOnce()
        {
            ApiRoom room = new ApiRoom()
            {
                RoomId = Guid.NewGuid()
            };

            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;

            var mockHttpWrapper = new Mock<IHttpClientWrapper>();
            mockHttpWrapper.Setup(x => x.PutAsync<ApiRoom>(It.IsAny<String>(), It.IsAny<ApiRoom>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetRoomServicePath()).Returns("string");

            ServiceRoomCalls src = new ServiceRoomCalls(mockHttpWrapper.Object, mockApiPath.Object);

            await src.UpdateRoomAsync(room);

            mockHttpWrapper.Verify(x => x.PutAsync<ApiRoom>(It.IsAny<String>(), It.IsAny<ApiRoom>()),
                Times.Once);
        }

        [Fact]
        public async Task UpdateRoomAsync_UpdateRoomNotValid_ThrowException()
        {
            ApiRoom room = new ApiRoom()
            {
                RoomId = Guid.Empty
            };

            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;

            var mockHttpWrapper = new Mock<IHttpClientWrapper>();
            mockHttpWrapper.Setup(x => x.PutAsync<ApiRoom>(It.IsAny<String>(), It.IsAny<ApiRoom>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetRoomServicePath()).Returns("string");

            ServiceRoomCalls src = new ServiceRoomCalls(mockHttpWrapper.Object, mockApiPath.Object);

            await Assert.ThrowsAsync<Exception>(() => src.UpdateRoomAsync(room));
        }

        [Fact]
        public async Task UpdateRoomAsync_UpdateRoomFails_ThrowException()
        {
            ApiRoom room = new ApiRoom()
            {
                RoomId = Guid.NewGuid()
            };

            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.BadRequest;

            var mockHttpWrapper = new Mock<IHttpClientWrapper>();
            mockHttpWrapper.Setup(x => x.PutAsync<ApiRoom>(It.IsAny<String>(), It.IsAny<ApiRoom>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetRoomServicePath()).Returns("string");

            ServiceRoomCalls src = new ServiceRoomCalls(mockHttpWrapper.Object, mockApiPath.Object);

            await Assert.ThrowsAsync<Exception>(() => src.UpdateRoomAsync(room));
        }

        [Fact]
        public async Task GetAllUsers_StatusCodeSuccessReturnsUsers_ReturnsApiUsers()
        {
            List<ApiUser> apiUsersexpected = new List<ApiUser>();
            apiUsersexpected.Add(new ApiUser());

            var response = new HttpResponseMessage();
            response.Content = new ObjectContent<List<ApiUser>>(apiUsersexpected, new JsonMediaTypeFormatter(), "application/json");

            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(x => x.GetAsync(It.IsAny<String>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetRoomServicePath()).Returns("string");

            ServiceUserCalls sur = new ServiceUserCalls(mockHttpClientWrapper.Object, mockApiPath.Object);

            var actual = await sur.RetrieveAllUsersAsync();

            Assert.Equal(apiUsersexpected, actual);
        }

        [Fact]
        public async Task GetAllUsers_StatusCodeSuccessNoRooms_ReturnsNull()
        {
            List<ApiUser> apiUsersexpected = new List<ApiUser>();

            var response = new HttpResponseMessage();
            response.Content = new ObjectContent<List<ApiUser>>(apiUsersexpected, new JsonMediaTypeFormatter(), "application/json");

            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(x => x.GetAsync(It.IsAny<String>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetRoomServicePath()).Returns("string");

            ServiceUserCalls sur = new ServiceUserCalls(mockHttpClientWrapper.Object, mockApiPath.Object);

            var actual = await sur.RetrieveAllUsersAsync();

            Assert.Null(actual);
        }

        [Fact]
        public async Task GetAllUsers_StatusCodeFail_ReturnsNull()
        {
            List<ApiUser> apiUsersexpected = new List<ApiUser>();
            apiUsersexpected.Add(new ApiUser());

            var response = new HttpResponseMessage();
            response.Content = new ObjectContent<List<ApiUser>>(apiUsersexpected, new JsonMediaTypeFormatter(), "application/json");
            response.StatusCode = HttpStatusCode.NotFound;

            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(x => x.GetAsync(It.IsAny<String>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetRoomServicePath()).Returns("string");

            ServiceUserCalls sur = new ServiceUserCalls(mockHttpClientWrapper.Object, mockApiPath.Object);

            var actual = await sur.RetrieveAllUsersAsync();

            Assert.Null(actual);
        }

        [Fact]
        public async Task UpdateUserAsync_UpdateUserValidAllInputs_PutIsCalledOnce()
        {
            ApiUser user = new ApiUser()
            {
                UserId = Guid.NewGuid(),
                Location = "Earth",
                Address = new ApiAddress()
                {
                    AddressId = Guid.NewGuid(),
                    Address1 = "123 1st street",
                    City = "City",
                    State = "FL",
                    Country = "US",
                    PostalCode = "12345"
                }
            };

            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;

            var mockHttpWrapper = new Mock<IHttpClientWrapper>();
            mockHttpWrapper.Setup(x => x.PutAsync<ApiUser>(It.IsAny<String>(), It.IsAny<ApiUser>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetUserServicePath()).Returns("string");

            ServiceUserCalls src = new ServiceUserCalls(mockHttpWrapper.Object, mockApiPath.Object);

            await src.UpdateUserAsync(user);

            mockHttpWrapper.Verify(x => x.PutAsync<ApiUser>(It.IsAny<String>(), It.IsAny<ApiUser>()),
                Times.Once);
        }

        [Fact]
        public async Task UpdateUserAsync_UpdateUserValidNoLocation_PutIsCalledOnce()
        {
            ApiUser user = new ApiUser()
            {
                UserId = Guid.NewGuid(),
                Address = new ApiAddress()
                {
                    AddressId = Guid.NewGuid(),
                    Address1 = "123 1st street",
                    City = "City",
                    State = "FL",
                    Country = "US",
                    PostalCode = "12345"
                }
            };

            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;

            var mockHttpWrapper = new Mock<IHttpClientWrapper>();
            mockHttpWrapper.Setup(x => x.PutAsync<ApiUser>(It.IsAny<String>(), It.IsAny<ApiUser>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetUserServicePath()).Returns("string");

            ServiceUserCalls src = new ServiceUserCalls(mockHttpWrapper.Object, mockApiPath.Object);

            await src.UpdateUserAsync(user);

            mockHttpWrapper.Verify(x => x.PutAsync<ApiUser>(It.IsAny<String>(), It.IsAny<ApiUser>()),
                Times.Once);
        }

        [Fact]
        public async Task UpdateUserAsync_UpdateUserValidNullAddress_PutIsCalledOnce()
        {
            ApiUser user = new ApiUser()
            {
                UserId = Guid.NewGuid(),
                Location = "Earth",
            };

            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;

            var mockHttpWrapper = new Mock<IHttpClientWrapper>();
            mockHttpWrapper.Setup(x => x.PutAsync<ApiUser>(It.IsAny<String>(), It.IsAny<ApiUser>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetUserServicePath()).Returns("string");

            ServiceUserCalls src = new ServiceUserCalls(mockHttpWrapper.Object, mockApiPath.Object);

            await src.UpdateUserAsync(user);

            mockHttpWrapper.Verify(x => x.PutAsync<ApiUser>(It.IsAny<String>(), It.IsAny<ApiUser>()),
                Times.Once);
        }

        [Fact]
        public async Task UpdateUserAsync_UpdateUserInvalidId_ThrowsException()
        {
            ApiUser user = new ApiUser()
            {
                UserId = Guid.Empty,
                Location = "Earth",
                Address = new ApiAddress()
                {
                    AddressId = Guid.NewGuid(),
                    Address1 = "123 1st street",
                    City = "City",
                    State = "FL",
                    Country = "US",
                    PostalCode = "12345"
                }
            };

            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;

            var mockHttpWrapper = new Mock<IHttpClientWrapper>();
            mockHttpWrapper.Setup(x => x.PutAsync<ApiUser>(It.IsAny<String>(), It.IsAny<ApiUser>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetUserServicePath()).Returns("string");

            ServiceUserCalls src = new ServiceUserCalls(mockHttpWrapper.Object, mockApiPath.Object);

            await Assert.ThrowsAsync<Exception>(() => src.UpdateUserAsync(user));
        }

        [Fact]
        public async Task UpdateUserAsync_UpdateUserInvalidLocation_ThrowsException()
        {
            ApiUser user = new ApiUser()
            {
                UserId = Guid.NewGuid(),
                Location = "",
                Address = new ApiAddress()
                {
                    AddressId = Guid.NewGuid(),
                    Address1 = "123 1st street",
                    City = "City",
                    State = "FL",
                    Country = "US",
                    PostalCode = "12345"
                }
            };

            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;

            var mockHttpWrapper = new Mock<IHttpClientWrapper>();
            mockHttpWrapper.Setup(x => x.PutAsync<ApiUser>(It.IsAny<String>(), It.IsAny<ApiUser>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetUserServicePath()).Returns("string");

            ServiceUserCalls src = new ServiceUserCalls(mockHttpWrapper.Object, mockApiPath.Object);

            await Assert.ThrowsAsync<Exception>(() => src.UpdateUserAsync(user));
        }

        [Fact]
        public async Task UpdateUserAsync_UpdateUserInvalidAddress_ThrowsException()
        {
            ApiUser user = new ApiUser()
            {
                UserId = Guid.NewGuid(),
                Location = "Earth",
                Address = new ApiAddress()
                {
                    AddressId = Guid.NewGuid(),
                    Address1 = "123 1st street",
                    State = "FL",
                    Country = "US",
                    PostalCode = "12345"
                }
            };

            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;

            var mockHttpWrapper = new Mock<IHttpClientWrapper>();
            mockHttpWrapper.Setup(x => x.PutAsync<ApiUser>(It.IsAny<String>(), It.IsAny<ApiUser>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetUserServicePath()).Returns("string");

            ServiceUserCalls src = new ServiceUserCalls(mockHttpWrapper.Object, mockApiPath.Object);

            await Assert.ThrowsAsync<Exception>(() => src.UpdateUserAsync(user));
        }
    }
}
