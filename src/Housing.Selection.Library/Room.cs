using Housing.Selection.Library.HousingModels;
using System;
using System.Collections.Generic;

namespace Housing.Selection.Library
{
    public class Room
    {
        private const int MaxStringLength = 255;
        /// <summary>
        /// Our primary key.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Service hub primary key.
        /// </summary>
        public Guid RoomId { get; set; }

        public string Location { get; set; }

        public int Vacancy { get; set; }
        
        public int Occupancy { get; set; }

        public string Gender { get; set; }
        
        public Address Address { get; set; }

        public ICollection<User> Users { get; set; }

        public bool IsValidState()
        {
            if (RoomId == Guid.Empty) { return false; }
            if (string.IsNullOrEmpty(Location) || Location?.Length > MaxStringLength || Location?.Length <= 0) { return false; }
            if (Occupancy <= 0) { return false; }
            if (Vacancy > Occupancy || Vacancy < 0) { return false; }
            if (Gender == null) { return false; }

            return true;
        }
    }
}
