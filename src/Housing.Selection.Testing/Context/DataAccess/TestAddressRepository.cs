using System;
using System.Collections.Generic;
using System.Threading;
using Housing.Selection.Context.DataAccess;
using Housing.Selection.Library.HousingModels;
using Moq;
using Xunit;

namespace Housing.Selection.Testing.Context.DataAccess
{
    public class TestAddressRepository
    {
        private readonly IDbContext _mockHousingContext;
        private readonly List<Address> _addressList = new List<Address>();
        private readonly Address _testAddress1 = new Address();
        private readonly Address _testAddress2 = new Address();
        private readonly Guid _guid;
        private readonly Guid _guid1;

        public TestAddressRepository()
        {
            var mockHousingContext = new Mock<IDbContext>();

            _guid = Guid.NewGuid();
            _guid1 = Guid.NewGuid();

            _testAddress1.Id = _guid;
            _testAddress1.AddressId = _guid1;

            _testAddress2.Id = _guid1;
            _testAddress2.AddressId = _guid;


            _addressList.Add(_testAddress1);
            _addressList.Add(_testAddress2);

            var myDbSet = TestingUtilities.GetQueryableMockDbSet(_addressList);

            mockHousingContext.Setup(x => x.Addresses).Returns(myDbSet);

            _mockHousingContext = mockHousingContext.Object;
        }

        [Fact]
        public void CanAddAddress()
        {
            var mockHousingContext = new Mock<IDbContext>();

            var testAddress1 = new Address()
            {
                Id = _guid,
                AddressId = _guid

            };
            var addressList = new List<Address>()
            {
                 testAddress1
            };
            var myDbSet = TestingUtilities.GetQueryableMockDbSet(addressList);

            mockHousingContext.Setup(x => x.Addresses).Returns(myDbSet);

            mockHousingContext.Setup(x => x.Addresses.Add(It.IsAny<Address>()));

            var addressRepository = new AddressRepository(mockHousingContext.Object);

            addressRepository.AddAddress(testAddress1);

            mockHousingContext.Verify(m => m.Addresses.Add(It.IsAny<Address>()), Times.Once());
        }

        [Fact]
        public void CanReturnAddresses()
        {
            var addressRepository = new AddressRepository(_mockHousingContext);

            var testAddresses = addressRepository.GetAddresses();

            Assert.NotNull(testAddresses);
        }

        [Fact]
        public async void CanSaveChangesAsync()
        {
            var mockHousingContext = new Mock<IDbContext>();

            mockHousingContext.Setup(x => x.SaveChanges()).Returns(1);

            var addressRepository = new AddressRepository(mockHousingContext.Object);

            await addressRepository.SaveChangesAsync();

            mockHousingContext.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once());
        }
    }
}
