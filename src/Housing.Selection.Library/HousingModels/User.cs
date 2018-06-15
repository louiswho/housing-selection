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
