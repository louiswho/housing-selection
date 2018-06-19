using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Library.HousingModels
{
    public class Name
    {
        private const int MaxNameLength = 255;
        /// <summary>
        /// Our primary key.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Service hub primary key.
        /// </summary>
        public Guid NameId { get; set; }

        public string First { get; set; }

        public string Middle { get; set; }

        public string Last { get; set; }

        public ICollection<User> Users { get; set; }

        /// <summary>
        /// Check that the Name object represents a valid name.
        /// </summary>
        /// <remarks>
        /// First and Last names are required, as well as Name Id. 
        /// Middle name is optional.
        /// No name fields can be an empty string.
        /// </remarks>
        /// <returns>True if the name is valid and false otherwise.</returns>
        public bool Validate()
        {
            return (NameId != Guid.Empty &&
                    ValidateFirstOrLastName(First) &&
                    ValidateMiddle(Middle) &&
                    ValidateFirstOrLastName(Last));
        }

        /// <summary>
        /// Check that the string parameter would represent a valid first name.
        /// </summary>
        /// <remarks>
        /// First name is required, must not be empty, and must not exceed FirstMaxLength.
        /// </remarks>
        /// <returns>True if the first name is valid and false otherwise.</returns>
        public static bool ValidateFirstOrLastName(string name)
        {
            return !string.IsNullOrEmpty(name) && name.Length <= MaxNameLength;
        }

        /// <summary>
        /// Check that the string parameter would represent a valid middle name.
        /// </summary>
        /// <remarks>
        /// Middle name is not required.
        /// Middle name must not be empty if it is not null.
        /// Middle name must not exceed MiddleMaxLength.
        /// </remarks>
        /// <returns>True if the middle name is valid and false otherwise.</returns>
        public static bool ValidateMiddle(string middleName)
        {
            if (middleName == null)
            {
                return true;
            }
            return middleName.Length <= MaxNameLength && middleName != "";
        }

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
