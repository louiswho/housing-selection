using System;
using System.Collections.Generic;

namespace Housing.Selection.Library
{
    public class Batch
    {
        public Guid Id { get; set; }
        public Guid BatchId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string BatchName { get; set; }
        public int BatchOccupancy { get; set; }
        public string BatchSkill { get; set; }

        public ICollection<User> Users { get; set; }

        public Address Address { get; set; }
    }
}
