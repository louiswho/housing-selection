using System;
using System.Collections.Generic;

namespace Housing.Selection.Library
{
    public class Batch
    {
        /// <summary>
        /// Our primary key.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Service hub primary key.
        /// </summary>
        public Guid BatchId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string BatchName { get; set; }

        public int BatchOccupancy { get; set; }

        public string BatchSkill { get; set; }

        public string State { get; set; }

        public ICollection<User> Users { get; set; }

        /// <summary>
        /// Check if object has any invalid (default) property values
        /// </summary>
        /// <returns>Returns false if any value is invalid, else true</returns>
        public bool Validate()
        {
            if (BatchId == Guid.Empty) { return false; }
            if (string.IsNullOrEmpty(BatchName)) { return false; }
            if (string.IsNullOrEmpty(State)) { return false; }
            if (BatchOccupancy < 0 || BatchOccupancy > 100) { return false; }
            if (string.IsNullOrEmpty(BatchSkill)) { return false; }

            return true;
        }
    }
}