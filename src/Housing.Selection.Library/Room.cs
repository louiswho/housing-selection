using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Housing.Selection.Library
{
    public class Room
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
        public Guid RoomId { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public int Vacancy { get; set; }
        
        [Required]
        public int Occupancy { get; set; }

        [Required]
        public char Gender { get; set; }
        
        [Required]
        public Address Address { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
