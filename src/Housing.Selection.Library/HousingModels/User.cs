using System;
using System.ComponentModel.DataAnnotations;

namespace Housing.Selection.Library
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
    }
}
