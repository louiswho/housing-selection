using Housing.Selection.Library.HousingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Housing.Selection.Context.DataAccess
{
   public class AddressRepository : IAddressRepository
    {
        private readonly IDbContext _housingSelectionDbContext;

        public AddressRepository(IDbContext housingSelectionContext)
        {
            _housingSelectionDbContext = housingSelectionContext;
        }

        public void AddAddress(Address address)
        {
            _housingSelectionDbContext.Addresses.Add(address);
        }

        public IEnumerable<Address> GetAddresses()
        {
            return _housingSelectionDbContext.Addresses;
        }

        public Address GetAddressById(Guid id)
        {
            return _housingSelectionDbContext.Addresses.First(x => x.Id == id);
        }

        public Address GetAddressByAddressId(Guid addressId)
        {
            return _housingSelectionDbContext.Addresses.First(x => x.AddressId == addressId);
        }

        public async Task  SaveChangesAsync()
        {
            _housingSelectionDbContext.SaveChangesAsync();
        }
    }
}
