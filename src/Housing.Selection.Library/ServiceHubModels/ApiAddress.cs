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

        public static explicit operator Address(ApiAddress apiAddress)
        {
            Address address = new Address()
            {
                AddressId = apiAddress.AddressId,
                Address1 = apiAddress.Address1,
                Address2 = apiAddress.Address2,
                City = apiAddress.City,
                State = apiAddress.State,
                PostalCode = apiAddress.PostalCode,
                Country = apiAddress.Country
            };
            return address;
        }
    }
}
