using Housing.Selection.Context.DataAccess;
using Housing.Selection.Library.HousingModels;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
//Clean up white space.
namespace Housing.Selection.Testing.Context
{
    public class TestBatchRepository
    {
        private readonly IDbContext mockHousingContext;
        private List<Batch> batchList = new List<Batch>();
        private Batch testBatch1 = new Batch();
        private Batch testBatch2 = new Batch();
        private Guid guid;
        private Guid guid1;        

        public TestBatchRepository()
        {           
            var  mockHousingContext = new Mock<IDbContext>();
            
            guid =  Guid.NewGuid(); 
            guid1 = Guid.NewGuid();

            testBatch1.Id = guid;
            testBatch1.BatchId = guid1;

            testBatch2.Id = guid1;
            testBatch2.BatchId = guid;


            batchList.Add(testBatch1);
            batchList.Add(testBatch2);

            DbSet<Batch> myDbSet = TestingUtilities.GetQueryableMockDbSet(batchList);

            mockHousingContext.Setup(x => x.Batches).Returns(myDbSet);

            this.mockHousingContext = mockHousingContext.Object;
        }

        [Fact]
        public void CanAddBatch()
        {
            Mock<IDbContext>  MockHousingContext = new Mock<IDbContext>();

            var testBatch1 = new Batch()
            {
                Id = guid,
                BatchId = guid

            };
            var  batchList = new List<Batch>()
            {
                  testBatch1
            };
            DbSet<Batch> myDbSet = TestingUtilities.GetQueryableMockDbSet(batchList);

            MockHousingContext.Setup(x => x.Batches).Returns(myDbSet);

            MockHousingContext.Setup(x => x.Batches.Add(It.IsAny<Batch>()));

            var batchRepository = new BatchRepository(MockHousingContext.Object);

            batchRepository.AddBatch(testBatch1);

            MockHousingContext.Verify(m => m.Batches.Add(It.IsAny<Batch>()), Times.Once());
        }

        [Fact]
        public void CanReturnBatches()
        {     
            var batchRepository = new BatchRepository(mockHousingContext);

            var testBatches = batchRepository.GetBatches();

            Assert.NotNull(testBatches); 
        }

        [Fact]
        public void CanReturnBatchesById()
        {
            var batchRepository = new BatchRepository(mockHousingContext);

            var testBatch = batchRepository.GetBatchByBatchId(guid);

            Assert.Equal(guid1, testBatch.Id);
        }

        [Fact]
        public void CanSaveChanges()
        {                    
            var  MockHousingContext = new Mock<IDbContext>();

            MockHousingContext.Setup(x => x.saveChanges()).Returns(1);

            var roomRepository = new UserRepository(MockHousingContext.Object);

            roomRepository.SaveChanges();

            MockHousingContext.Verify(m => m.saveChanges(), Times.Once());
        }
    }
}
