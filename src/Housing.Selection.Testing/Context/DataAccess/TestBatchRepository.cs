using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Housing.Selection.Context.DataAccess;
using Housing.Selection.Library.HousingModels;
using Moq;
using Xunit;

namespace Housing.Selection.Testing.Context.DataAccess
{
    public class TestBatchRepository
    {
        private readonly IDbContext _mockHousingContext;
        private readonly List<Batch> _batchList = new List<Batch>();
        private readonly Batch _testBatch1 = new Batch();
        private readonly Batch _testBatch2 = new Batch();
        private readonly Guid _guid;
        private readonly Guid _guid1;

        public TestBatchRepository()
        {
            var mockHousingContext = new Mock<IDbContext>();

            _guid = Guid.NewGuid();
            _guid1 = Guid.NewGuid();

            _testBatch1.Id = _guid;
            _testBatch1.BatchId = _guid1;

            _testBatch2.Id = _guid1;
            _testBatch2.BatchId = _guid;


            _batchList.Add(_testBatch1);
            _batchList.Add(_testBatch2);

            var myDbSet = TestingUtilities.GetQueryableMockDbSet(_batchList);

            mockHousingContext.Setup(x => x.Batches).Returns(myDbSet);

            _mockHousingContext = mockHousingContext.Object;
        }

        [Fact]
        public void CanAddBatch()
        {
            var mockHousingContext = new Mock<IDbContext>();

            var testBatch1 = new Batch()
            {
                Id = _guid,
                BatchId = _guid

            };
            var batchList = new List<Batch>()
            {
                  testBatch1
            };
            var myDbSet = TestingUtilities.GetQueryableMockDbSet(batchList);

            mockHousingContext.Setup(x => x.Batches).Returns(myDbSet);

            mockHousingContext.Setup(x => x.Batches.Add(It.IsAny<Batch>()));

            var batchRepository = new BatchRepository(mockHousingContext.Object);

            batchRepository.AddBatch(testBatch1);

            mockHousingContext.Verify(m => m.Batches.Add(It.IsAny<Batch>()), Times.Once());
        }

        [Fact]
        public void CanReturnBatches()
        {
            var batchRepository = new BatchRepository(_mockHousingContext);

            var testBatches = batchRepository.GetBatches();

            Assert.NotNull(testBatches);
        }

        [Fact]
        public async void CanSaveChanges()
        {
            var mockHousingContext = new Mock<IDbContext>();

            mockHousingContext.Setup(x => x.SaveChanges()).Returns(1);

            var roomRepository = new UserRepository(mockHousingContext.Object);

            await roomRepository.SaveChangesAsync();

            mockHousingContext.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once());
        }
    }
}
