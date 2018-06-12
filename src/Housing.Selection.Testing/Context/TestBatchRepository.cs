using Housing.Selection.Context.DataAccess;
using Housing.Selection.Library.HousingModels;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Housing.Selection.Testing.Context
{
    public class TestBatchRepository
    {
        public readonly IBatchRepository mockBatchRepository;

        public TestBatchRepository()
        {
            Mock<IBatchRepository> mockBatchRepository = new Mock<IBatchRepository>();

            var guid = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");
            var guid1 = new Guid("62FA647C-AD54-4BCC-A867-R6Y2664B056");
            Batch batch = new Batch()
            {
                BatchId = guid,
                BatchName = ".Net 2018 5",
                BatchOccupancy = 34,
                BatchSkill = ".NET",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now

            };
            Batch batch1 = new Batch()
            {
                BatchId = guid,
                BatchName = "Java 2018 5",
                BatchOccupancy = 25,
                BatchSkill = "Java",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now

            };
            List<Batch> BatchList = new List<Batch>
        {
            batch,
            batch1
        };
           
           


            mockBatchRepository.Setup(x => x.GetBatches()).Returns(BatchList);

            // return a batch by Id
            mockBatchRepository.Setup(mr => mr.GetBatchByBatchId(
                It.IsAny<Guid>())).Returns((Guid id) => BatchList.Where(
                x => x.BatchId == id).Single());
            this.mockBatchRepository = mockBatchRepository.Object;

        }

        [Fact]
        public void CanReturnBatchById()
        {
            var guid = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");
            // Try finding a batch by id

            Batch testBatch = this.mockBatchRepository.GetBatchByBatchId(guid);

            Assert.NotNull(testBatch); // Test if null
            Assert.IsType<Batch>(testBatch); // Test type
            Assert.Equal(".Net 2018 5", testBatch.BatchName); // Verify it is the right batch.
        }
    }


}
