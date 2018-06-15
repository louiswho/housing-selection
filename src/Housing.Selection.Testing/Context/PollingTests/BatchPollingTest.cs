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
    public class BatchPollingTest
    {
        private Batch batch1, batch2, batch3;
        private ApiBatch apiBatch1, apiBatch2;
        private List<Batch> mockBatchList;
        private Task<List<ApiBatch>> mockApiBatchList;
        private Mock<IBatchRepository> mockBatchRepo;

        private PollBatch pollBatch;

        public BatchPollingTest()
        {
            PollingSetupBatch();
            
            mockBatchRepo = new Mock<IBatchRepository>();
            mockBatchRepo.Setup(x => x.GetBatchByBatchId(It.IsAny<Guid>())).Returns(batch1);
            var mockBatchRetrieval = new Mock<IServiceBatchCalls>();
            mockBatchRetrieval.Setup(x => x.RetrieveAllBatchesAsync()).Returns(mockApiBatchList);

            pollBatch = new PollBatch(mockBatchRepo.Object, mockBatchRetrieval.Object);
        }

        [Fact]
        public async void Test_Batch_Poll()
        {
            mockBatchList.Add(batch1);
            mockBatchList.Add(batch1);
            var expected = mockBatchList;
            var result = await pollBatch.BatchPoll();

            Assert.Equal(expected, result);
        }

        [Fact]
        public async void Test_Batch_Poll_Fail()
        {
            mockBatchList.Add(batch1);
            mockBatchList.Add(batch1);
            mockBatchList.Add(batch2);
            var expected = mockBatchList;
            var result = await pollBatch.BatchPoll();

            Assert.NotEqual(expected, result);
        }

        [Fact]
        public void Test_Batch_Update()
        {
            var expected = batch1;
            var result = pollBatch.UpdateBatch(apiBatch1);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Batch_Update_Fail()
        {
            var expected = batch2;
            var result = pollBatch.UpdateBatch(apiBatch1);

            Assert.NotEqual(expected, result);
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
            batch2 = new Batch()
            {
                Id = Guid.NewGuid(),
                BatchId = Guid.NewGuid(),
                StartDate = DateTime.Today,
                EndDate = DateTime.Today,
                BatchName = "Batch Two",
                BatchOccupancy = 2,
                BatchSkill = "None",
                Address = new Address()
                {
                    Id = Guid.NewGuid(),
                    AddressId = Guid.NewGuid(),
                    Address1 = "222 Batch2 St",
                    City = "Tampa",
                    State = "FL",
                    PostalCode = "22222",
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
            mockBatchList = new List<Batch>();            
            List<ApiBatch> apiBatchList = new List<ApiBatch>();
            apiBatchList.Add(apiBatch1);
            apiBatchList.Add(apiBatch2);
            mockApiBatchList = Task.FromResult<List<ApiBatch>>(apiBatchList);
        }
    }
}