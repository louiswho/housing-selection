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

            ServiceBatchRetrieval sbr = new ServiceBatchRetrieval(mockHttpClientWrapper.Object, mockApiPath.Object);

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

            ServiceBatchRetrieval sbr = new ServiceBatchRetrieval(mockHttpClientWrapper.Object, mockApiPath.Object);

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

            ServiceBatchRetrieval sbr = new ServiceBatchRetrieval(mockHttpClientWrapper.Object, mockApiPath.Object);

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

            ServiceRoomRetrieval sbr = new ServiceRoomRetrieval(mockHttpClientWrapper.Object, mockApiPath.Object);

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

            ServiceRoomRetrieval srr = new ServiceRoomRetrieval(mockHttpClientWrapper.Object, mockApiPath.Object);

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

            ServiceRoomRetrieval srr = new ServiceRoomRetrieval(mockHttpClientWrapper.Object, mockApiPath.Object);

            var actual = await srr.RetrieveAllRoomsAsync();

            Assert.Null(actual);
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

            ServiceUserRetrieval sur = new ServiceUserRetrieval(mockHttpClientWrapper.Object, mockApiPath.Object);

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

            ServiceUserRetrieval sur = new ServiceUserRetrieval(mockHttpClientWrapper.Object, mockApiPath.Object);

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

            ServiceUserRetrieval sur = new ServiceUserRetrieval(mockHttpClientWrapper.Object, mockApiPath.Object);

            var actual = await sur.RetrieveAllUsersAsync();

            Assert.Null(actual);
        }
    }
}
