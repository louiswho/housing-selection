using Housing.Selection.Library.HousingModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Housing.Selection.Context.DataAccess
{
   public interface IAddressRepository
    {
        IEnumerable<Address> GetAddresses();
        Task <Address> GetAddressById(Guid id);
        Task <Address> GetAddressByAddressId(Guid nameId);
        void AddAddress(Address address);
        Task SaveChanges();
    }
}
