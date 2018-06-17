using Housing.Selection.Library.HousingModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Selection.Context.DataAccess
{
   public interface IAddressRepository
    {
        IEnumerable<Address> GetAddresses();
        Address GetAddressById(Guid id);
        Address GetAddressByAddressId(Guid nameId);
        void AddAddress(Address address);
        void SaveChanges();
    }
}
