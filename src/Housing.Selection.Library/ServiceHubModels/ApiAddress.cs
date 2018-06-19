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

        /// <summary>
        /// Use this method to update a housing Address with the properties from the
        /// calling service hub Address
        /// doesnt change any nav properties from the passed in Address
        /// </summary>
        /// <param name="oldAddress">An Address object is passed into this method.
        /// Updates the housing Address properties to match the ones grabbed from the
        /// api call.
        /// All other fields are ignored.
        /// </param>
        /// <returns>
        /// Address that has been updated with the calling ApiAddress's properties
        /// </returns>
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

        /// <summary>
        /// Use this method to create a new Address in the instance
        /// where housing is passed a service hub Address that does not exist
        /// in housings DB
        /// </summary> 
        /// <returns>
        /// Returns a new Address object
        /// </returns>       
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
