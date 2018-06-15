using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Library.HousingModels
{
    public class Address
    {
        /// <summary>
        /// Our primary key.
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Service hub primary key.
        /// </summary>
        [Required]
        public Guid AddressId { get; set; }

        [Required]
        [StringLength(255)]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required]
        [StringLength(25)]
        public string City { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string State { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 5)]
        public string PostalCode { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string Country { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<Room> Rooms { get; set; }

        /// <summary>
        /// Use this method to convert from a 
        /// service hub Address, to a housing Address
        /// leaves nav properties the same
        /// </summary>
        /// <param name="apiAddress">An ApiAddress object is passed into this method.
        /// Updates the housing Address properties to match the ones grabbed from the
        /// api call.
        /// All other fields are ignored.
        /// </param>
        public Address ConvertFromServiceModel(ApiAddress apiAddress)
        {
            var housingAddress = this;
            housingAddress.AddressId = apiAddress.AddressId;
            housingAddress.Address1 = apiAddress.Address1;
            housingAddress.Address2 = apiAddress.Address2;
            housingAddress.City = apiAddress.City;
            housingAddress.State = apiAddress.State;
            housingAddress.PostalCode = apiAddress.PostalCode;
            housingAddress.Country = apiAddress.Country;

            return housingAddress;
        }
    }
}