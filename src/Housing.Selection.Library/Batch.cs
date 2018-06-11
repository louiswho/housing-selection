﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Housing.Selection.Library
{
    public class Batch
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

        [Required]
        public Address Address { get; set; }
    }
}