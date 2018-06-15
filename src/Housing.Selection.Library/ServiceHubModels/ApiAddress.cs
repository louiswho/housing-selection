using System;
using Housing.Selection.Library.HousingModels;

namespace Housing.Selection.Library.ServiceHubModels
{
    public class ApiAddress
    {
        public Guid AddressId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public Address ConvertToAddress(Address oldAddress)
        {
            oldAddress.AddressId = this.AddressId;
            oldAddress.Address1 = this.Address1;
            oldAddress.Address2 = this.Address2;
            oldAddress.City = this.City;
            oldAddress.State = this.State;
            oldAddress.PostalCode = this.PostalCode;
            oldAddress.Country = this.Country;

            return oldAddress;
        }

        public Address CreateNewAddress()
        {
            Address address = new Address()
            {
                Id = Guid.NewGuid(),
                AddressId = this.AddressId,
                Address1 = this.Address1,
                Address2 = this.Address2,
                City = this.City,
                State = this.State,
                PostalCode = this.PostalCode,
                Country = this.Country
            };
            return address;
        }
    }
}
