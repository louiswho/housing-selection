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

        #region Constructor
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
                BatchName = ".Net 2018 5"
            };
            Batch batch1 = new Batch()
            {
                BatchId = guid1,
                BatchName = "Java 2018 5"
              
            };
            List<Batch> BatchList = new List<Batch>
        {
            batch,
            batch1
        };

            DbSet<Batch> myDbSet = TestingUtilities.GetQueryableMockDbSet(BatchList);

            mockHousingContext.Setup(x => x.Batches).Returns(myDbSet);

            this.mockHousingContext = mockHousingContext.Object;

        }
        #endregion

        [Fact]
        public void CanAddBatch()
        {

            Mock<IDbContext> MockHousingContext = new Mock<IDbContext>();


            var guid = new Guid("CF0A8C1C-F2D0-41A1-A12C-53D9BE513A1E");
            Address address = new Address();
            Batch testBatch1 = new Batch()
            {
                Id = guid,
                BatchId = guid

            };
            List<Batch> batchList = new List<Batch>()
            {
                  testBatch1
            };
            DbSet<Batch> myDbSet = TestingUtilities.GetQueryableMockDbSet(batchList);

            MockHousingContext.Setup(x => x.Batches).Returns(myDbSet);

            MockHousingContext.Setup(x => x.Batches.Add(It.IsAny<Batch>()));


            BatchRepository batchRepository = new BatchRepository(MockHousingContext.Object);

            batchRepository.AddBatch(testBatch1);

            MockHousingContext.Verify(m => m.Batches.Add(It.IsAny<Batch>()), Times.Once());

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

        [Fact]
        public void CanSaveChanges()
        {
            Mock<IDbContext> MockHousingContext = new Mock<IDbContext>();
            MockHousingContext.Setup(x => x.saveChanges()).Returns(1);

            UserRepository roomRepository = new UserRepository(MockHousingContext.Object);
            roomRepository.SaveChanges();

            MockHousingContext.Verify(m => m.saveChanges(), Times.Once());
        }
    }
}
