using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Housing.Selection.Library.Models
{
    public class Room
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid RoomID { get; set; }

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
