using System;
using System.ComponentModel.DataAnnotations;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Library.HousingModels
{
    public class User
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
        public Guid UserId { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Gender { get; set; }

        public string Type { get; set; }

        [Required]
        public Batch Batch { get; set; }

        public Room Room { get; set; }

        [Required]
        public Name Name { get; set; }

        public Address Address { get; set; }

        /// <summary>
        /// Use this method to convert from a 
        /// service hub User, to a housing User
        /// leaves nav properties the same
        /// </summary>
        /// <param name="apiUser">An ApiUser object is passed into this method.
        /// Updates the housing User properties to match the ones grabbed from the
        /// api call.
        /// All other fields are ignored.
        /// </param>
        /// <returns>
        /// User that has been updated with the ApiUsers properties
        /// </returns>
        public User ConvertFromServiceModel(ApiUser apiUser)
        {
            User housingUser = this;
            housingUser.UserId = apiUser.UserId;
            housingUser.Location = apiUser.Location;
            housingUser.Email = apiUser.Email;
            housingUser.Name = apiUser.Name.ConvertToName(this.Name);
            housingUser.Gender = apiUser.Gender.ToString();
            housingUser.Type = apiUser.Type;

            return housingUser;
        }


        /// <summary>
        /// Use this method to create a new User in the instance
        /// where housing is passed a service hub User that does not exist
        /// in housings DB
        /// </summary>
        /// <param name="apiUser">An ApiUser object is passed into this method.
        /// Creates a new housing User with properties to match the ones grabbed from the
        /// api call.
        /// All other fields are ignored.
        /// </param>
        /// <returns>
        /// New instance of User that has been created with the ApiUsers properties
        /// </returns>
        public User NewUserFromServiceModel(ApiUser apiUser)
        {
            User housingUser = new User()
            {
                Id = Guid.NewGuid(),
                UserId = apiUser.UserId,
                Location = apiUser.Location,
                Address = apiUser.Address.ConvertToAddress(this.Address),
                Email = apiUser.Email,
                Name = apiUser.Name.ConvertToName(this.Name),
                Gender = apiUser.Gender.ToString(),
                Type = apiUser.Type
            };

            return housingUser;
        }
    }
}
