using System;
using System.Collections.Generic;

namespace Housing.Selection.Library.Models
{
    public class Room
    {
        public Guid Id { get; set; }
        public Guid RoomID { get; set; }
        public string Location { get; set; }
        public int Vacancy { get; set; }
        public int Occupancy { get; set; }
        public char Gender { get; set; }
        
        public Address Address { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
