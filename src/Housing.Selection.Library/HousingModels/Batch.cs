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

        /// <summary>
        /// Use this method to convert from a 
        /// service hub Batch, to a housing Batch
        /// leaves nav properties the same
        /// </summary>
        /// <param name="apiBatch">An ApiBatch object is passed into this method.
        /// Updates the housing Batch properties to match the ones grabbed from the
        /// api call.
        /// All other fields are ignored.
        /// </param>
        public Batch ConvertFromServiceModel(ApiBatch apiBatch)
        {
            Batch housingBatch = this;
            housingBatch.BatchId = apiBatch.BatchId;
            housingBatch.StartDate = (DateTime)apiBatch.StartDate;
            housingBatch.EndDate = (DateTime)apiBatch.EndDate;
            housingBatch.BatchName = apiBatch.BatchName;
            housingBatch.BatchOccupancy = (int)apiBatch.BatchOccupancy;
            housingBatch.BatchSkill = apiBatch.BatchSkill;
            housingBatch.Address = apiBatch.Address.ConvertToAddress(housingBatch.Address);
            
            return housingBatch;
        }

        /// <summary>
        /// Use this method to create a new Batch in the instance
        /// where housing is passed a service hub Batch that does not exist
        /// in housings DB
        /// </summary>
        /// <param name="apiBatch">An ApiBatch object is passed into this method.
        /// Creates a new housing Batch with properties to match the ones grabbed from the
        /// api call.
        /// All other fields are ignored.
        /// </param>
        public Batch CreateNewBatch(ApiBatch apiBatch)
        {
            Batch batch = new Batch()
            {
                BatchId = apiBatch.BatchId,
                StartDate = (DateTime)apiBatch.StartDate,
                EndDate = (DateTime)apiBatch.EndDate,
                BatchName = apiBatch.BatchName,
                BatchOccupancy = (int)apiBatch.BatchOccupancy,
                BatchSkill = apiBatch.BatchSkill,
                Address = apiBatch.Address.CreateNewAddress()
            };
           
            return batch;
        }
    }
}