using System;
using System.Collections.Generic;
using System.Threading;
using Housing.Selection.Context.DataAccess;
using Housing.Selection.Library.HousingModels;
using Moq;
using Xunit;

namespace Housing.Selection.Testing.Context.DataAccess
{
    public class TestNameRepository
    {
        private readonly IDbContext _mockHousingContext;
        private readonly List<Name> _nameList = new List<Name>();
        private readonly Name _testName1 = new Name();
        private readonly Name _testName2 = new Name();
        private readonly Guid _guid;
        private readonly Guid _guid1;

        public TestNameRepository()
        {
            var mockHousingContext = new Mock<IDbContext>();

            _guid = Guid.NewGuid();
            _guid1 = Guid.NewGuid();

            _testName1.Id = _guid;
            _testName1.NameId = _guid1;

            _testName2.Id = _guid1;
            _testName2.NameId = _guid;


            _nameList.Add(_testName1);
            _nameList.Add(_testName2);

            var myDbSet = TestingUtilities.GetQueryableMockDbSet(_nameList);

            mockHousingContext.Setup(x => x.Names).Returns(myDbSet);

            _mockHousingContext = mockHousingContext.Object;
        }

        [Fact]
        public void CanAddName()
        {
            var mockHousingContext = new Mock<IDbContext>();

            var testName1 = new Name()
            {
                Id = _guid,
                NameId = _guid

            };
            var nameList = new List<Name>()
            {
                  testName1
            };
            var myDbSet = TestingUtilities.GetQueryableMockDbSet(nameList);

            mockHousingContext.Setup(x => x.Names).Returns(myDbSet);

            mockHousingContext.Setup(x => x.Names.Add(It.IsAny<Name>()));

            var nameRepository = new NameRepository(mockHousingContext.Object);

            nameRepository.AddName(testName1);

            mockHousingContext.Verify(m => m.Names.Add(It.IsAny<Name>()), Times.Once());
        }

        [Fact]
        public void CanReturnNames()
        {
            var nameRepository = new NameRepository(_mockHousingContext);

            var testNames = nameRepository.GetNames();

            Assert.NotNull(testNames);
        }

        [Fact]
        public async void CanSaveChangesAsync()
        {
            var mockHousingContext = new Mock<IDbContext>();

            mockHousingContext.Setup(x => x.SaveChanges()).Returns(1);

            var nameRepository = new NameRepository(mockHousingContext.Object);

            await nameRepository.SaveChangesAsync();

            mockHousingContext.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once());
        }
    }
}
