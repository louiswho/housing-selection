using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Library.HousingModels
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
        public Batch ConvertFromServiceModel(ApiBatch apiBatch)
        {
            Batch housingBatch = new Batch();            
            housingBatch.BatchId = apiBatch.BatchId;
            housingBatch.StartDate = (DateTime) apiBatch.StartDate;
            housingBatch.EndDate = (DateTime) apiBatch.EndDate;            
            housingBatch.BatchName = apiBatch.BatchName;
            housingBatch.BatchOccupancy = (int) apiBatch.BatchOccupancy;
            housingBatch.BatchSkill = apiBatch.BatchSkill;
            housingBatch.Address = (Address) apiBatch.Address;
            //TODO - Figure out how to handle apiBatch userIds
            return housingBatch;
        }
    }
}