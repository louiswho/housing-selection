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
        [Fact]
        public async Task GetAllBatches_TestingSuccess_ReturnsApiBatches()
        {
            // Arrange
            List<ApiBatch> apibatchesexpected = new List<ApiBatch>();

            var mockHttpResponse = new Mock<HttpResponseMessage>();
            mockHttpResponse.Setup(x => x.Content.ReadAsAsync<List<ApiBatch>>()).ReturnsAsync(apibatchesexpected);
            mockHttpResponse.Setup(x => x.IsSuccessStatusCode).Returns(true);

            var mockIHttpClient = new Mock<IHttpClientWrapper>();
            mockIHttpClient.Setup(x => x.GetAsync(It.IsAny<String>())).ReturnsAsync(mockHttpResponse.Object);

            var mockApiPath = new Mock<ApiPathBuilder>();
            mockApiPath.Setup(x => x.GetBatchServicePath()).Returns("string");

            ServiceBatchRetrieval sbr = new ServiceBatchRetrieval(mockIHttpClient.Object, mockApiPath.Object);

            var actual = await sbr.RetrieveAllBatchesAsync();

            Assert.Equal(apibatchesexpected, actual);
        }

        [Fact]
        public async Task GetAllBatches_TestingFail_ReturnsNull()
        {
            // Arrange
            List<ApiBatch> apibatchesexpected = new List<ApiBatch>();

            var mockHttpResponse = new Mock<HttpResponseMessage>();
            mockHttpResponse.Setup(x => x.Content.ReadAsAsync<List<ApiBatch>>()).ReturnsAsync(apibatchesexpected);
            mockHttpResponse.Setup(x => x.IsSuccessStatusCode).Returns(false);

            var mockIHttpClient = new Mock<IHttpClientWrapper>();
            mockIHttpClient.Setup(x => x.GetAsync(It.IsAny<String>())).ReturnsAsync(mockHttpResponse.Object);

            var mockApiPath = new Mock<ApiPathBuilder>();
            mockApiPath.Setup(x => x.GetBatchServicePath()).Returns("string");

            ServiceBatchRetrieval sbr = new ServiceBatchRetrieval(mockIHttpClient.Object, mockApiPath.Object);

            var actual = await sbr.RetrieveAllBatchesAsync();

            Assert.Null(actual);
        }
    }
}
