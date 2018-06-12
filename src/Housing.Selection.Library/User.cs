using System;
using System.ComponentModel.DataAnnotations;

namespace Housing.Selection.Library
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
    }
}
