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
            Batch housingBatch = this;            
            housingBatch.BatchId = apiBatch.BatchId;
            housingBatch.StartDate = (DateTime) apiBatch.StartDate;
            housingBatch.EndDate = (DateTime) apiBatch.EndDate;            
            housingBatch.BatchName = apiBatch.BatchName;
            housingBatch.BatchOccupancy = (int) apiBatch.BatchOccupancy;
            housingBatch.BatchSkill = apiBatch.BatchSkill;
            housingBatch.Address = apiBatch.Address.ConvertToAddress(housingBatch.Address);
            //TODO - Figure out how to handle apiBatch userIds
            return housingBatch;
        }

        public Batch CreateNewBatch(ApiBatch apiBatch)
        {
            Batch batch = new Batch()
            {
                BatchId = apiBatch.BatchId,
                StartDate = (DateTime) apiBatch.StartDate,
                EndDate = (DateTime) apiBatch.EndDate,
                BatchName = apiBatch.BatchName,
                BatchOccupancy = (int) apiBatch.BatchOccupancy,
                BatchSkill = apiBatch.BatchSkill,
                Address = apiBatch.Address.CreateNewAddress()
            };
            //TODO - Figure out how to handle apiBatch userIds
            return batch;
        }
    }
}