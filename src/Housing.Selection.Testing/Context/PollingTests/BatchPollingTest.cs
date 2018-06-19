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
        private Batch batch1, batch2;
        private ApiBatch apiBatch1, apiBatch2;
        private List<Batch> mockBatchList;
        private Task<List<ApiBatch>> mockApiBatchList;
        private Mock<IBatchRepository> mockBatchRepo;

        private PollBatch pollBatch;

        public BatchPollingTest()
        {
            PollingSetupBatch();
            
            mockBatchRepo = new Mock<IBatchRepository>();
            mockBatchRepo.Setup(x => x.GetBatchByBatchId(It.IsAny<Guid>())).Returns(Task.FromResult<Batch>(batch1));
            var mockBatchRetrieval = new Mock<IServiceBatchCalls>();
            mockBatchRetrieval.Setup(x => x.RetrieveAllBatchesAsync()).Returns(mockApiBatchList);

            pollBatch = new PollBatch(mockBatchRepo.Object, mockBatchRetrieval.Object);
        }

        [Fact]
        public async void Test_Batch_Poll()
        {
            var mockTaskBatchList = new List<Batch>();
            mockTaskBatchList.Add(batch1);
            mockTaskBatchList.Add(batch1);
            var expected = mockTaskBatchList;
            var result = await pollBatch.BatchPollAsync();

            Assert.Equal(expected, result);
        }

        [Fact]
        public async void Test_Batch_Poll_Fail()
        {
            mockBatchList.Add(batch1);
            mockBatchList.Add(batch1);
            mockBatchList.Add(batch2);
            var expected = mockBatchList;
            var result = await pollBatch.BatchPollAsync();

            Assert.NotEqual(expected, result);
        }

        [Fact]
        public async void Test_Batch_Update()
        {
            var expected = batch1;
            var result = await pollBatch.UpdateBatchAsync(apiBatch1);

            Assert.Equal(expected, result);
        }

        [Fact]
        public async void Test_Batch_Update_Fail()
        {
            var expected = batch2;
            var result = await pollBatch.UpdateBatchAsync(apiBatch1);

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
                Location = "USF"
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
                Location = "Tampa"
            };
            apiBatch1 = new ApiBatch()
            {
                BatchId = Guid.NewGuid(),
                StartDate = DateTime.Today,
                EndDate = DateTime.Today,
                BatchName = "Batch One",
                BatchOccupancy = 1,
                BatchSkill = "None",
                Location = "Reston"
            };
            apiBatch2 = new ApiBatch()
            {
                BatchId = Guid.NewGuid(),
                StartDate = DateTime.Today,
                EndDate = DateTime.Today,
                BatchName = "Batch Two",
                BatchOccupancy = 2,
                BatchSkill = "None",
                Location = "Virginia"
            };
            mockBatchList = new List<Batch>();            
            List<ApiBatch> apiBatchList = new List<ApiBatch>();
            apiBatchList.Add(apiBatch1);
            apiBatchList.Add(apiBatch2);
            mockApiBatchList = Task.FromResult<List<ApiBatch>>(apiBatchList);
        }
    }
}