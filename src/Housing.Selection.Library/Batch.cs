using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Selection.Library
{
    public class Batch
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid BatchId { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [StringLength(50)]
        public string BatchName { get; set; }
        [Range(0, 100)]
        public int BatchOccupancy { get; set; }
        [StringLength(50)]
        public string BatchSkill { get; set; }

        public ICollection<User> Users { get; set; }

        public Address Address { get; set; }
    }
}
