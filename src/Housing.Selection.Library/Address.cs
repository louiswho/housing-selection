using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Housing.Selection.Library
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

        public ICollection<Batch> Batches { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}