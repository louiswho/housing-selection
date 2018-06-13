using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using System.Net.Http;
using Housing.Selection.Context.HttpRequests;
using System.Threading.Tasks;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Testing.Context
{
    public class HttpRequestsTests
    {
        #region ServiceBatchRetrieval Tests
        [Fact]
        public async Task GetAllBatches_StatusCodeSuccessBatchReturned_ReturnsApiBatches()
        {
            List<ApiBatch> apibatchesexpected = new List<ApiBatch>();
            apibatchesexpected.Add(new ApiBatch());

            var mockHttpResponse = new Mock<HttpResponseMessage>();
            mockHttpResponse.Setup(x => x.Content.ReadAsAsync<List<ApiBatch>>()).ReturnsAsync(apibatchesexpected);
            mockHttpResponse.Setup(x => x.IsSuccessStatusCode).Returns(true);

            var mockHttpClient = new Mock<HttpClient>();
            mockHttpClient.Setup(x => x.GetAsync(It.IsAny<String>())).ReturnsAsync(mockHttpResponse.Object);

            var mockApiPath = new Mock<ApiPathBuilder>();
            mockApiPath.Setup(x => x.GetBatchServicePath()).Returns("string");

            ServiceBatchRetrieval sbr = new ServiceBatchRetrieval(mockHttpClient.Object, mockApiPath.Object);

            var actual = await sbr.RetrieveAllBatchesAsync();

            Assert.Equal(apibatchesexpected, actual);
        }

        [Fact]
        public async Task GetAllBatches_StatusCodeSuccessNoBatches_ReturnsNull()
        {
            List<ApiBatch> apibatchesexpected = new List<ApiBatch>();

            var mockHttpResponse = new Mock<HttpResponseMessage>();
            mockHttpResponse.Setup(x => x.Content.ReadAsAsync<List<ApiBatch>>()).ReturnsAsync(apibatchesexpected);
            mockHttpResponse.Setup(x => x.IsSuccessStatusCode).Returns(true);

            var mockHttpClient = new Mock<HttpClient>();
            mockHttpClient.Setup(x => x.GetAsync(It.IsAny<String>())).ReturnsAsync(mockHttpResponse.Object);

            var mockApiPath = new Mock<ApiPathBuilder>();
            mockApiPath.Setup(x => x.GetBatchServicePath()).Returns("string");

            ServiceBatchRetrieval sbr = new ServiceBatchRetrieval(mockHttpClient.Object, mockApiPath.Object);

            var actual = await sbr.RetrieveAllBatchesAsync();

            Assert.Null(actual);
        }

        [Fact]
        public async Task GetAllBatches_StatusCodeFail_ReturnsNull()
        {
            List<ApiBatch> apibatchesexpected = new List<ApiBatch>();

            var mockHttpResponse = new Mock<HttpResponseMessage>();
            mockHttpResponse.Setup(x => x.Content.ReadAsAsync<List<ApiBatch>>()).ReturnsAsync(apibatchesexpected);
            mockHttpResponse.Setup(x => x.IsSuccessStatusCode).Returns(false);

            var mockHttpClient = new Mock<HttpClient>();
            mockHttpClient.Setup(x => x.GetAsync(It.IsAny<String>())).ReturnsAsync(mockHttpResponse.Object);

            var mockApiPath = new Mock<ApiPathBuilder>();
            mockApiPath.Setup(x => x.GetBatchServicePath()).Returns("string");

            ServiceBatchRetrieval sbr = new ServiceBatchRetrieval(mockHttpClient.Object, mockApiPath.Object);

            var actual = await sbr.RetrieveAllBatchesAsync();

            Assert.Null(actual);
        }
        #endregion

        #region ServiceRoomRetrieval Tests
        [Fact]
        public async Task GetAllRooms_StatusCodeSuccessReturnsRoom_ReturnsApiRooms()
        {
            List<ApiRoom> apiroomsexpected = new List<ApiRoom>();
            apiroomsexpected.Add(new ApiRoom());

            var mockHttpResponse = new Mock<HttpResponseMessage>();
            mockHttpResponse.Setup(x => x.Content.ReadAsAsync<List<ApiRoom>>()).ReturnsAsync(apiroomsexpected);
            mockHttpResponse.Setup(x => x.IsSuccessStatusCode).Returns(true);

            var mockHttpClient = new Mock<HttpClient>();
            mockHttpClient.Setup(x => x.GetAsync(It.IsAny<String>())).ReturnsAsync(mockHttpResponse.Object);

            var mockApiPath = new Mock<ApiPathBuilder>();
            mockApiPath.Setup(x => x.GetRoomServicePath()).Returns("string");

            ServiceRoomRetrieval sbr = new ServiceRoomRetrieval(mockHttpClient.Object, mockApiPath.Object);

            var actual = await sbr.RetrieveAllRoomsAsync();

            Assert.Equal(apiroomsexpected, actual);
        }

        [Fact]
        public async Task GetAllRooms_StatusCodeSuccessNoRooms_ReturnsNull()
        {
            List<ApiRoom> apiroomsexpected = new List<ApiRoom>();

            var mockHttpResponse = new Mock<HttpResponseMessage>();
            mockHttpResponse.Setup(x => x.Content.ReadAsAsync<List<ApiRoom>>()).ReturnsAsync(apiroomsexpected);
            mockHttpResponse.Setup(x => x.IsSuccessStatusCode).Returns(true);

            var mockHttpClient = new Mock<HttpClient>();
            mockHttpClient.Setup(x => x.GetAsync(It.IsAny<String>())).ReturnsAsync(mockHttpResponse.Object);

            var mockApiPath = new Mock<ApiPathBuilder>();
            mockApiPath.Setup(x => x.GetRoomServicePath()).Returns("string");

            ServiceRoomRetrieval srr = new ServiceRoomRetrieval(mockHttpClient.Object, mockApiPath.Object);

            var actual = await srr.RetrieveAllRoomsAsync();

            Assert.Null(actual);
        }

        [Fact]
        public async Task GetAllRooms_StatusCodeFail_ReturnsNull()
        {
            var mockHttpResponse = new Mock<HttpResponseMessage>();
            mockHttpResponse.Setup(x => x.IsSuccessStatusCode).Returns(false);

            var mockHttpClient = new Mock<HttpClient>();
            mockHttpClient.Setup(x => x.GetAsync(It.IsAny<String>())).ReturnsAsync(mockHttpResponse.Object);

            var mockApiPath = new Mock<ApiPathBuilder>();
            mockApiPath.Setup(x => x.GetRoomServicePath()).Returns("string");

            ServiceRoomRetrieval srr = new ServiceRoomRetrieval(mockHttpClient.Object, mockApiPath.Object);

            var actual = await srr.RetrieveAllRoomsAsync();

            Assert.Null(actual);
        }
        #endregion

        #region ServiceUserRetrieval Tests
        [Fact]
        public async Task GetAllUsers_StatusCodeSuccessReturnsUsers_ReturnsApiUsers()
        {
            List<ApiUser> apiUsersexpected = new List<ApiUser>();
            apiUsersexpected.Add(new ApiUser());

            var mockHttpResponse = new Mock<HttpResponseMessage>();
            mockHttpResponse.Setup(x => x.Content.ReadAsAsync<List<ApiUser>>()).ReturnsAsync(apiUsersexpected);
            mockHttpResponse.Setup(x => x.IsSuccessStatusCode).Returns(true);

            var mockHttpClient = new Mock<HttpClient>();
            mockHttpClient.Setup(x => x.GetAsync(It.IsAny<String>())).ReturnsAsync(mockHttpResponse.Object);

            var mockApiPath = new Mock<ApiPathBuilder>();
            mockApiPath.Setup(x => x.GetUserServicePath()).Returns("string");

            ServiceUserRetrieval sur = new ServiceUserRetrieval(mockHttpClient.Object, mockApiPath.Object);

            var actual = await sur.RetrieveAllUsersAsync();

            Assert.Equal(apiUsersexpected, actual);
        }

        [Fact]
        public async Task GetAllUsers_StatusCodeSuccessNoRooms_ReturnsNull()
        {
            List<ApiUser> apiUsersexpected = new List<ApiUser>();

            var mockHttpResponse = new Mock<HttpResponseMessage>();
            mockHttpResponse.Setup(x => x.Content.ReadAsAsync<List<ApiUser>>()).ReturnsAsync(apiUsersexpected);
            mockHttpResponse.Setup(x => x.IsSuccessStatusCode).Returns(true);

            var mockHttpClient = new Mock<HttpClient>();
            mockHttpClient.Setup(x => x.GetAsync(It.IsAny<String>())).ReturnsAsync(mockHttpResponse.Object);

            var mockApiPath = new Mock<ApiPathBuilder>();
            mockApiPath.Setup(x => x.GetUserServicePath()).Returns("string");

            ServiceUserRetrieval sur = new ServiceUserRetrieval(mockHttpClient.Object, mockApiPath.Object);

            var actual = await sur.RetrieveAllUsersAsync();

            Assert.Null(actual);
        }

        [Fact]
        public async Task GetAllUsers_StatusCodeFail_ReturnsNull()
        {
            var mockHttpResponse = new Mock<HttpResponseMessage>();
            mockHttpResponse.Setup(x => x.IsSuccessStatusCode).Returns(false);

            var mockHttpClient = new Mock<HttpClient>();
            mockHttpClient.Setup(x => x.GetAsync(It.IsAny<String>())).ReturnsAsync(mockHttpResponse.Object);

            var mockApiPath = new Mock<ApiPathBuilder>();
            mockApiPath.Setup(x => x.GetUserServicePath()).Returns("string");

            ServiceUserRetrieval sur = new ServiceUserRetrieval(mockHttpClient.Object, mockApiPath.Object);

            var actual = await sur.RetrieveAllUsersAsync();

            Assert.Null(actual);
        }
        #endregion
    }

}
