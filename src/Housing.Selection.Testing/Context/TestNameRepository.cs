using Housing.Selection.Context.DataAccess;
using Housing.Selection.Library.HousingModels;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;



namespace Housing.Selection.Testing.Context
{
    public class TestNameRepository
    {
        private readonly IDbContext mockHousingContext;
        private List<Name> nameList = new List<Name>();
        private Name testName1 = new Name();
        private Name testName2 = new Name();
        private Guid guid;
        private Guid guid1;

        public TestNameRepository()
        {
            var mockHousingContext = new Mock<IDbContext>();

            guid = Guid.NewGuid();
            guid1 = Guid.NewGuid();

            testName1.Id = guid;
            testName1.NameId = guid1;

            testName2.Id = guid1;
            testName2.NameId = guid;


            nameList.Add(testName1);
            nameList.Add(testName2);

            DbSet<Name> myDbSet = TestingUtilities.GetQueryableMockDbSet(nameList);

            mockHousingContext.Setup(x => x.Names).Returns(myDbSet);

            this.mockHousingContext = mockHousingContext.Object;
        }

        [Fact]
        public void CanAddName()
        {
            Mock<IDbContext> MockHousingContext = new Mock<IDbContext>();

            var testName1 = new Name()
            {
                Id = guid,
                NameId = guid

            };
            var NameList = new List<Name>()
            {
                  testName1
            };
            DbSet<Name> myDbSet = TestingUtilities.GetQueryableMockDbSet(NameList);

            MockHousingContext.Setup(x => x.Names).Returns(myDbSet);

            MockHousingContext.Setup(x => x.Names.Add(It.IsAny<Name>()));

            var nameRepository = new NameRepository(MockHousingContext.Object);

            nameRepository.AddName(testName1);

            MockHousingContext.Verify(m => m.Names.Add(It.IsAny<Name>()), Times.Once());
        }

        [Fact]
        public void CanReturnNames()
        {
            var nameRepository = new NameRepository(mockHousingContext);

            var testNames = nameRepository.GetNames();

            Assert.NotNull(testNames);
        }

        [Fact]
        public void CanReturnNamesById()
        {
            var nameRepository = new NameRepository(mockHousingContext);

            var testName = nameRepository.GetNameById(guid);

            Assert.Equal(guid1, testName.NameId);
        }

        [Fact]
        public void CanSaveChanges()
        {
            var MockHousingContext = new Mock<IDbContext>();

            MockHousingContext.Setup(x => x.SaveChanges()).Returns(1);

            var nameRepository = new NameRepository(MockHousingContext.Object);

            nameRepository.SaveChanges();

            MockHousingContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
