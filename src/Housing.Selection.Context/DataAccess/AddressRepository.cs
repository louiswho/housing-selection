using Housing.Selection.Library.HousingModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            _housingSelectionDbContext.SaveChanges();
        }

        public IEnumerable<Address> GetAddresses()
        {
            return _housingSelectionDbContext.Addresses
                .Include(x => x.Rooms)
                .Include(y => y.Users);
        }

        public async Task<Address> GetAddressById(Guid id)
        {
            return await _housingSelectionDbContext.Addresses
                    .Include(x => x.Rooms)
                    .Include(y => y.Users)
                    .FirstAsync(x => x.Id == id);
        }

        public async Task<Address> GetAddressByAddressId(Guid addressId)
        {
            return await _housingSelectionDbContext.Addresses
                .Include(x => x.Rooms)
                .Include(y => y.Users)
                .FirstAsync(x => x.AddressId == addressId);
        }

        public async Task SaveChangesAsync()
        {
            await _housingSelectionDbContext.SaveChangesAsync();
        }
    }
}
