using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Library.HousingModels
{
    public class Name
    {
        /// <summary>
        /// Our primary key.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Service hub primary key.
        /// </summary>
        [Required]
        public Guid NameId { get; set; }

        [Required]
        public string First { get; set; }

        public string Middle { get; set; }

        [Required]
        public string Last { get; set; }

        public ICollection<User> Users { get; set; }

        /// <summary>
        /// Use this method to convert from a 
        /// service hub Name, to a housing Name
        /// leaves nav properties the same
        /// </summary>
        /// <param name="apiName">An ApiName object is passed into this method.
        /// Updates the housing Name properties to match the ones grabbed from the
        /// api call.
        /// All other fields are ignored.
        /// </param>
        /// <returns>
        /// Name that has been updated with the ApiName properties
        /// </returns>
        public Name ConvertFromServiceModel(ApiName apiName)
        {
            Name housingName = this;

            NameId = apiName.NameId;
            First = apiName.First;
            Middle = apiName.Middle;
            Last = apiName.Last;

            return housingName;
        }

        /// <summary>
        /// Use this method to create a new Name in the instance
        /// where housing is passed a service hub Name that does not exist
        /// in housings DB
        /// </summary>
        /// <param name="apiName">An ApiName object is passed into this method.
        /// Creates a new housing Name with properties to match the ones grabbed from the
        /// api call.
        /// All other fields are ignored.
        /// </param>
        /// <returns>
        /// New instance of Name that has been created with the ApiNames properties
        /// </returns>
        public Name NewNameFromServiceModel(ApiName apiName)
        {
            Name housingName = new Name()
            {
                Id = Guid.NewGuid(),
                NameId = apiName.NameId,
                First = apiName.First,
                Middle = apiName.Middle,
                Last = apiName.Last,
            };
            return housingName;
        }
    }
}
