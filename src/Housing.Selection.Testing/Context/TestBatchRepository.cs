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
        
            Mock<IDbContext> mockHousingContext = new Mock<IDbContext>();
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

            ///<summary>
            ///
            /// </summary>
            var dbSetAddress = new Mock<DbSet<Address>>();

            
            DbSet<Batch> myDbSet = GetQueryableMockDbSet(BatchList);
            myDbSet.Add(batch);
                   
            mockHousingContext.Setup(x => x.Addresses).Returns(dbSetAddress.Object);
            mockHousingContext.Setup(x => x.Batches).Returns(myDbSet);

            this.mockHousingContext = mockHousingContext.Object;

        }

        [Fact]
        public void CanReturnBatches()
        {
           

            BatchRepository batchRepository = new BatchRepository(mockHousingContext);
            // Try finding a batch by id

            var testBatches = batchRepository.GetBatches();
          
            Assert.NotNull(testBatches); // Test if null
            //Assert.IsType<Batch>(testBatch); // Test type
            //Assert.Equal(".Net 2018 5", testBatch.BatchName); // Verify it is the right batch.
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



        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }
    }
}
