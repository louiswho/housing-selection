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
            var apibatchesexpected = new List<ApiBatch> {new ApiBatch()};

            var response = new HttpResponseMessage {Content = new ObjectContent<List<ApiBatch>>(apibatchesexpected, new JsonMediaTypeFormatter(), "application/json")};

            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetBatchServicePath()).Returns("string");

            var sbr = new ServiceBatchCalls(mockHttpClientWrapper.Object, mockApiPath.Object);

            var actual = await sbr.RetrieveAllBatchesAsync();

            Assert.Equal(apibatchesexpected, actual);
            Assert.Single(actual);
        }

        [Fact]
        public async Task GetAllBatches_StatusCodeSuccessNoBatches_ReturnsNull()
        {
            var apibatchesexpected = new List<ApiBatch>();

            var response = new HttpResponseMessage {Content = new ObjectContent<List<ApiBatch>>(apibatchesexpected, new JsonMediaTypeFormatter(), "application/json")};

            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetBatchServicePath()).Returns("string");

            var sbr = new ServiceBatchCalls(mockHttpClientWrapper.Object, mockApiPath.Object);

            var actual = await sbr.RetrieveAllBatchesAsync();

            Assert.Null(actual);
        }

        [Fact]
        public async Task GetAllBatches_StatusCodeFail_ReturnsNull()
        {
            var apibatchesexpected = new List<ApiBatch> {new ApiBatch()};

            var response = new HttpResponseMessage
            {
                Content = new ObjectContent<List<ApiBatch>>(apibatchesexpected, new JsonMediaTypeFormatter(), "application/json"),
                StatusCode = HttpStatusCode.NotFound
            };

            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetBatchServicePath()).Returns("string");

            var sbr = new ServiceBatchCalls(mockHttpClientWrapper.Object, mockApiPath.Object);

            var actual = await sbr.RetrieveAllBatchesAsync();

            Assert.Null(actual);
        }

        [Fact]
        public async Task GetAllRooms_StatusCodeSuccessReturnsRooms_ReturnsApiRooms()
        {
            var apiroomsexpected = new List<ApiRoom> {new ApiRoom()};

            var response = new HttpResponseMessage {Content = new ObjectContent<List<ApiRoom>>(apiroomsexpected, new JsonMediaTypeFormatter(), "application/json")};

            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetRoomServicePath()).Returns("string");

            var sbr = new ServiceRoomCalls(mockHttpClientWrapper.Object, mockApiPath.Object);

            var actual = await sbr.RetrieveAllRoomsAsync();

            Assert.Equal(apiroomsexpected, actual);
            Assert.Single(actual);
        }

        [Fact]
        public async Task GetAllRooms_StatusCodeSuccessNoRooms_ReturnsNull()
        {
            var apiroomsexpected = new List<ApiRoom>();

            var response = new HttpResponseMessage {Content = new ObjectContent<List<ApiRoom>>(apiroomsexpected, new JsonMediaTypeFormatter(), "application/json")};

            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetRoomServicePath()).Returns("string");

            var srr = new ServiceRoomCalls(mockHttpClientWrapper.Object, mockApiPath.Object);

            var actual = await srr.RetrieveAllRoomsAsync();

            Assert.Null(actual);
        }

        [Fact]
        public async Task GetAllRooms_StatusCodeFail_ReturnsNull()
        {
            var apiroomsexpected = new List<ApiRoom> {new ApiRoom()};

            var response = new HttpResponseMessage
            {
                Content = new ObjectContent<List<ApiRoom>>(apiroomsexpected, new JsonMediaTypeFormatter(), "application/json"),
                StatusCode = HttpStatusCode.NotFound
            };

            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetRoomServicePath()).Returns("string");

            var srr = new ServiceRoomCalls(mockHttpClientWrapper.Object, mockApiPath.Object);

            var actual = await srr.RetrieveAllRoomsAsync();

            Assert.Null(actual);
        }

        [Fact]
        public async Task UpdateRoomAsync_UpdateRoomValid_PutIsCalledOnce()
        {
            var room = new ApiRoom()
            {
                RoomId = Guid.NewGuid()
            };

            var response = new HttpResponseMessage {StatusCode = HttpStatusCode.OK};

            var mockHttpWrapper = new Mock<IHttpClientWrapper>();
            mockHttpWrapper.Setup(x => x.PutAsync(It.IsAny<string>(), It.IsAny<ApiRoom>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetRoomServicePath()).Returns("string");

            var src = new ServiceRoomCalls(mockHttpWrapper.Object, mockApiPath.Object);

            await src.UpdateRoomAsync(room);

            mockHttpWrapper.Verify(x => x.PutAsync(It.IsAny<string>(), It.IsAny<ApiRoom>()),
                Times.Once);
        }

        [Fact]
        public async Task UpdateRoomAsync_UpdateRoomNotValid_ThrowException()
        {
            var room = new ApiRoom()
            {
                RoomId = Guid.Empty
            };

            var response = new HttpResponseMessage {StatusCode = HttpStatusCode.OK};

            var mockHttpWrapper = new Mock<IHttpClientWrapper>();
            mockHttpWrapper.Setup(x => x.PutAsync(It.IsAny<string>(), It.IsAny<ApiRoom>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetRoomServicePath()).Returns("string");

            var src = new ServiceRoomCalls(mockHttpWrapper.Object, mockApiPath.Object);

            await Assert.ThrowsAsync<Exception>(() => src.UpdateRoomAsync(room));
        }

        [Fact]
        public async Task UpdateRoomAsync_UpdateRoomFails_ThrowException()
        {
            var room = new ApiRoom()
            {
                RoomId = Guid.NewGuid()
            };

            var response = new HttpResponseMessage {StatusCode = HttpStatusCode.BadRequest};

            var mockHttpWrapper = new Mock<IHttpClientWrapper>();
            mockHttpWrapper.Setup(x => x.PutAsync(It.IsAny<string>(), It.IsAny<ApiRoom>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetRoomServicePath()).Returns("string");

            var src = new ServiceRoomCalls(mockHttpWrapper.Object, mockApiPath.Object);

            await Assert.ThrowsAsync<Exception>(() => src.UpdateRoomAsync(room));
        }

        [Fact]
        public async Task GetAllUsers_StatusCodeSuccessReturnsUsers_ReturnsApiUsers()
        {
            var apiUsersexpected = new List<ApiUser> {new ApiUser()};

            var response = new HttpResponseMessage {Content = new ObjectContent<List<ApiUser>>(apiUsersexpected, new JsonMediaTypeFormatter(), "application/json")};

            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetRoomServicePath()).Returns("string");

            var sur = new ServiceUserCalls(mockHttpClientWrapper.Object, mockApiPath.Object);

            var actual = await sur.RetrieveAllUsersAsync();

            Assert.Equal(apiUsersexpected, actual);
        }

        [Fact]
        public async Task GetAllUsers_StatusCodeSuccessNoRooms_ReturnsNull()
        {
            var apiUsersexpected = new List<ApiUser>();

            var response = new HttpResponseMessage {Content = new ObjectContent<List<ApiUser>>(apiUsersexpected, new JsonMediaTypeFormatter(), "application/json")};

            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetRoomServicePath()).Returns("string");

            var sur = new ServiceUserCalls(mockHttpClientWrapper.Object, mockApiPath.Object);

            var actual = await sur.RetrieveAllUsersAsync();

            Assert.Null(actual);
        }

        [Fact]
        public async Task GetAllUsers_StatusCodeFail_ReturnsNull()
        {
            var apiUsersexpected = new List<ApiUser> {new ApiUser()};

            var response = new HttpResponseMessage
            {
                Content = new ObjectContent<List<ApiUser>>(apiUsersexpected, new JsonMediaTypeFormatter(), "application/json"),
                StatusCode = HttpStatusCode.NotFound
            };

            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetRoomServicePath()).Returns("string");

            var sur = new ServiceUserCalls(mockHttpClientWrapper.Object, mockApiPath.Object);

            var actual = await sur.RetrieveAllUsersAsync();

            Assert.Null(actual);
        }

        [Fact]
        public async Task UpdateUserAsync_UpdateUserValidAllInputs_PutIsCalledOnce()
        {
            var user = new ApiUser
            {
                UserId = Guid.NewGuid(),
                Location = "Earth",
                Address = new ApiAddress
                {
                    AddressId = Guid.NewGuid(),
                    Address1 = "123 1st street",
                    City = "City",
                    State = "FL",
                    Country = "US",
                    PostalCode = "12345"
                }
            };

            var response = new HttpResponseMessage {StatusCode = HttpStatusCode.OK};

            var mockHttpWrapper = new Mock<IHttpClientWrapper>();
            mockHttpWrapper.Setup(x => x.PutAsync(It.IsAny<string>(), It.IsAny<ApiUser>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetUserServicePath()).Returns("string");

            var src = new ServiceUserCalls(mockHttpWrapper.Object, mockApiPath.Object);

            await src.UpdateUserAsync(user);

            mockHttpWrapper.Verify(x => x.PutAsync(It.IsAny<string>(), It.IsAny<ApiUser>()),
                Times.Once);
        }

        [Fact]
        public async Task UpdateUserAsync_UpdateUserValidNoLocation_PutIsCalledOnce()
        {
            var user = new ApiUser
            {
                UserId = Guid.NewGuid(),
                Address = new ApiAddress
                {
                    AddressId = Guid.NewGuid(),
                    Address1 = "123 1st street",
                    City = "City",
                    State = "FL",
                    Country = "US",
                    PostalCode = "12345"
                }
            };

            HttpResponseMessage response = new HttpResponseMessage {StatusCode = HttpStatusCode.OK};

            var mockHttpWrapper = new Mock<IHttpClientWrapper>();
            mockHttpWrapper.Setup(x => x.PutAsync(It.IsAny<string>(), It.IsAny<ApiUser>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetUserServicePath()).Returns("string");

            var src = new ServiceUserCalls(mockHttpWrapper.Object, mockApiPath.Object);

            await src.UpdateUserAsync(user);

            mockHttpWrapper.Verify(x => x.PutAsync(It.IsAny<string>(), It.IsAny<ApiUser>()),
                Times.Once);
        }

        [Fact]
        public async Task UpdateUserAsync_UpdateUserValidNullAddress_PutIsCalledOnce()
        {
            var user = new ApiUser()
            {
                UserId = Guid.NewGuid(),
                Location = "Earth",
            };

            var response = new HttpResponseMessage {StatusCode = HttpStatusCode.OK};

            var mockHttpWrapper = new Mock<IHttpClientWrapper>();
            mockHttpWrapper.Setup(x => x.PutAsync(It.IsAny<string>(), It.IsAny<ApiUser>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetUserServicePath()).Returns("string");

            var src = new ServiceUserCalls(mockHttpWrapper.Object, mockApiPath.Object);

            await src.UpdateUserAsync(user);

            mockHttpWrapper.Verify(x => x.PutAsync(It.IsAny<string>(), It.IsAny<ApiUser>()),
                Times.Once);
        }

        [Fact]
        public async Task UpdateUserAsync_UpdateUserInvalidId_ThrowsException()
        {
            var user = new ApiUser
            {
                UserId = Guid.Empty,
                Location = "Earth",
                Address = new ApiAddress
                {
                    AddressId = Guid.NewGuid(),
                    Address1 = "123 1st street",
                    City = "City",
                    State = "FL",
                    Country = "US",
                    PostalCode = "12345"
                }
            };

            var response = new HttpResponseMessage {StatusCode = HttpStatusCode.OK};

            var mockHttpWrapper = new Mock<IHttpClientWrapper>();
            mockHttpWrapper.Setup(x => x.PutAsync(It.IsAny<string>(), It.IsAny<ApiUser>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetUserServicePath()).Returns("string");

            var src = new ServiceUserCalls(mockHttpWrapper.Object, mockApiPath.Object);

            await Assert.ThrowsAsync<Exception>(() => src.UpdateUserAsync(user));
        }

        [Fact]
        public async Task UpdateUserAsync_UpdateUserInvalidLocation_ThrowsException()
        {
            var user = new ApiUser
            {
                UserId = Guid.NewGuid(),
                Location = "",
                Address = new ApiAddress
                {
                    AddressId = Guid.NewGuid(),
                    Address1 = "123 1st street",
                    City = "City",
                    State = "FL",
                    Country = "US",
                    PostalCode = "12345"
                }
            };

            var response = new HttpResponseMessage {StatusCode = HttpStatusCode.OK};

            var mockHttpWrapper = new Mock<IHttpClientWrapper>();
            mockHttpWrapper.Setup(x => x.PutAsync(It.IsAny<string>(), It.IsAny<ApiUser>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetUserServicePath()).Returns("string");

            var src = new ServiceUserCalls(mockHttpWrapper.Object, mockApiPath.Object);

            await Assert.ThrowsAsync<Exception>(() => src.UpdateUserAsync(user));
        }

        [Fact]
        public async Task UpdateUserAsync_UpdateUserInvalidAddress_ThrowsException()
        {
            var user = new ApiUser
            {
                UserId = Guid.NewGuid(),
                Location = "Earth",
                Address = new ApiAddress
                {
                    AddressId = Guid.NewGuid(),
                    Address1 = "123 1st street",
                    State = "FL",
                    Country = "US",
                    PostalCode = "12345"
                }
            };

            HttpResponseMessage response = new HttpResponseMessage {StatusCode = HttpStatusCode.OK};

            var mockHttpWrapper = new Mock<IHttpClientWrapper>();
            mockHttpWrapper.Setup(x => x.PutAsync(It.IsAny<string>(), It.IsAny<ApiUser>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetUserServicePath()).Returns("string");

            var src = new ServiceUserCalls(mockHttpWrapper.Object, mockApiPath.Object);

            await Assert.ThrowsAsync<Exception>(() => src.UpdateUserAsync(user));
        }

        [Fact]
        public async Task UpdateUserAsync_UpdateUserFailed_ThrowsException()
        {
            var user = new ApiUser
            {
                UserId = Guid.NewGuid(),
                Location = "Earth",
                Address = new ApiAddress
                {
                    AddressId = Guid.NewGuid(),
                    Address1 = "123 1st street",
                    City = "City",
                    State = "FL",
                    Country = "US",
                    PostalCode = "12345"
                }
            };

            var response = new HttpResponseMessage {StatusCode = HttpStatusCode.BadRequest};

            var mockHttpWrapper = new Mock<IHttpClientWrapper>();
            mockHttpWrapper.Setup(x => x.PutAsync(It.IsAny<string>(), It.IsAny<ApiUser>())).ReturnsAsync(response);

            var mockApiPath = new Mock<IApiPathBuilder>();
            mockApiPath.Setup(x => x.GetUserServicePath()).Returns("string");

            var src = new ServiceUserCalls(mockHttpWrapper.Object, mockApiPath.Object);

            await Assert.ThrowsAsync<Exception>(() => src.UpdateUserAsync(user));
        }
    }
}
