using Housing.Selection.Context.DataAccess;
using Housing.Selection.Library.HousingModels;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Selection.Testing.Context
{

   

    class TestHousingContext
    {
        private HousingSelectionContext testHousingContext;
        private IEnumerable<Batch> batches;
        public TestHousingContext()
        {
            var moqContext = new Mock<IHousingSelectionContext>();
            var dbSet = new Mock<DbSet<Batch>>();
            //DbSet<Batch> myDbSet = GetQueryableMockDbSet(BatchList);
            moqContext.Setup(x => x.GetBatches()).Returns(dbSet.Object);
            testHousingContext = new HousingSelectionContext(moqContext.Object);
        }
    }


    //private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
    //{
    //    var queryable = sourceList.AsQueryable();

    //    var dbSet = new Mock<DbSet<T>>();
    //    dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
    //    dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
    //    dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
    //    dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
    //    dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

    //    return dbSet.Object;
    //}
}
