using Housing.Selection.Context.DataAccess;
using Housing.Selection.Library.HousingModels;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Housing.Selection.Testing.Context
{
  public  class TestAddressRepository
    {
        private readonly IDbContext mockHousingContext;
        private List<Address> addressList = new List<Address>();
        private Address testAddress1 = new Address();
        private Address testAddress2 = new Address();
        private Guid guid;
        private Guid guid1;

        public TestAddressRepository()
        {
            var mockHousingContext = new Mock<IDbContext>();

            guid = Guid.NewGuid();
            guid1 = Guid.NewGuid();

            testAddress1.Id = guid;
            testAddress1.AddressId = guid1;

            testAddress2.Id = guid1;
            testAddress2.AddressId = guid;


            addressList.Add(testAddress1);
            addressList.Add(testAddress2);

            DbSet<Address> myDbSet = TestingUtilities.GetQueryableMockDbSet(addressList);

            mockHousingContext.Setup(x => x.Addresses).Returns(myDbSet);

            this.mockHousingContext = mockHousingContext.Object;
        }

        [Fact]
        public void CanAddAddress()
        {
            Mock<IDbContext> MockHousingContext = new Mock<IDbContext>();

            var testAddress1 = new Address()
            {
                Id = guid,
               AddressId = guid

            };
            var addressList = new List<Address>()
            {
                 testAddress1
            };
            DbSet<Address> myDbSet = TestingUtilities.GetQueryableMockDbSet(addressList);

            MockHousingContext.Setup(x => x.Addresses).Returns(myDbSet);

            MockHousingContext.Setup(x => x.Addresses.Add(It.IsAny<Address>()));

            var addressRepository = new AddressRepository(MockHousingContext.Object);

            addressRepository.AddAddress(testAddress1);

            MockHousingContext.Verify(m => m.Addresses.Add(It.IsAny<Address>()), Times.Once());
        }

        [Fact]
        public void CanReturnAddresses()
        {
            var addressRepository = new AddressRepository(mockHousingContext);

            var testAddresses = addressRepository.GetAddresses();

            Assert.NotNull(testAddresses);
        }

        [Fact]
        public void CanReturnAddressesById()
        {
            var addressRepository = new AddressRepository(mockHousingContext);

            var testAddress = addressRepository.GetAddressById(guid);

            Assert.Equal(guid1, testAddress.AddressId);
        }

        [Fact]
        public void CanSaveChanges()
        {
            var MockHousingContext = new Mock<IDbContext>();

            MockHousingContext.Setup(x => x.saveChanges()).Returns(1);

            var addressRepository = new AddressRepository(MockHousingContext.Object);

            addressRepository.SaveChanges();

            MockHousingContext.Verify(m => m.saveChanges(), Times.Once());
        }
    }
}
