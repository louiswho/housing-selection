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
        public readonly IDbContext mockHousingContext;

        public TestBatchRepository()
        {
          // create a mock Db Context
            Mock<IDbContext> mockHousingContext = new Mock<IDbContext>();

            // Add fake data to the context
            var guid = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");
            var guid1 = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019F");
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
                BatchId = guid1,
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
            
            DbSet<Batch> myDbSet =  TestingUtilities.GetQueryableMockDbSet(BatchList);
         
            mockHousingContext.Setup(x => x.Batches).Returns(myDbSet);

            this.mockHousingContext = mockHousingContext.Object;

        }

        [Fact]
        public void CanReturnBatches()
        {
           
            BatchRepository batchRepository = new BatchRepository(mockHousingContext);
          
            // Try finding all batches
            var testBatches = batchRepository.GetBatches();
          
            Assert.NotNull(testBatches); // Test if null
        }

        [Fact]
        public void CanReturnBatchesById()
        {
            BatchRepository batchRepository = new BatchRepository(mockHousingContext);
         
            var guid = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");
            // Try finding a batch by id
            var testBatch = batchRepository.GetBatchByBatchId(guid);
            Assert.Equal(".Net 2018 5", testBatch.BatchName); // Verify it is the right batch.
        }
    }
}
