using Housing.Selection.Library.HousingModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        public async Task<Address> GetAddressById(Guid id)
        {
            return await _housingSelectionDbContext.Addresses.FirstAsync(x => x.Id == id);
        }

        public async Task<Address> GetAddressByAddressId(Guid addressId)
        {
            return await _housingSelectionDbContext.Addresses.FirstAsync(x => x.AddressId == addressId);
        }

        public async Task SaveChanges()
        {
            await _housingSelectionDbContext.SaveChangesAsync();
        }
    }
}
