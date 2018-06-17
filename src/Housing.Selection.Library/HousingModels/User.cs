using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Library.HousingModels
{
    public class User
    {
        /// <summary>
        /// Our primary key.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Service hub primary key.
        /// </summary>
        public Guid UserId { get; set; }

        public string Location { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public string Type { get; set; }

        [Required]
        public Batch Batch { get; set; }

        public Room Room { get; set; }

        [Required]
        public Name Name { get; set; }

        public Address Address { get; set; }

        /// <summary>
        /// Returns true if the User model is valid, and false otherwise.
        /// </summary>
        /// <remarks>
        /// All fields are required except Address, thus if any field besides
        /// Address is null or the Guid is the default value, the model is invalid.
        /// If Address is not null, the Address must be valid for the User to 
        /// be valid (via Address's validate method)
        /// Name must be valid for the User to be valid also (via Name's validate method).
        /// Location and Type must be 255 characters or less, and Email 254 or less.
        /// </remarks>
        /// <returns>True if user model is valid and false if invalid.</returns>
        public bool Validate()
        {
            const int MaxStringLength = 255;

            if (UserId == Guid.Empty) { return false; }
            if (string.IsNullOrEmpty(Location) || Location.Length > MaxStringLength) { return false; }
            if (ValidateEmail() == false || Email.Length > MaxStringLength) { return false; }
            if (Gender == null || ValidateGender() == false) { return false; }

            return true;
        }

        /// <summary>
        /// Check if Email is null, empty string or an invalid email address.
        /// If any of those are true, the email is invalid. Otherwise it is valid.
        /// </summary>
        /// <returns>True if the email is valid and false otherwise.</returns>
        public bool ValidateEmail()
        {
            try
            {
                var emailAddress = new MailAddress(Email);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check that the Gender field is a recognizable way to represent
        /// Male or Female.
        /// </summary>
        /// <remarks>
        /// Valid gender strings are "M", "Male", "F", "Female". (all case-insensitive)
        /// </remarks>
        /// <returns>True if the Gender is valid and false otherwise.</returns>
        public bool ValidateGender()
        {
            var validGenders = new List<string> { "M", "F", "MALE", "FEMALE" };
            return validGenders.Contains(Gender.ToUpper());
        }

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
