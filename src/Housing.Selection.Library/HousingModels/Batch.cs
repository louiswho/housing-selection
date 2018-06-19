using System;
using System.Collections.Generic;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Library.HousingModels
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

        public string Location { get; set; }

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
        /// <returns>
        /// Batch that has been updated with the ApiBatchs properties
        /// </returns>
        public Batch ConvertFromServiceModel(ApiBatch apiBatch)
        {
            Batch housingBatch = this;
            housingBatch.BatchId = apiBatch.BatchId;
            housingBatch.StartDate = (DateTime)apiBatch.StartDate;
            housingBatch.EndDate = (DateTime)apiBatch.EndDate;
            housingBatch.BatchName = apiBatch.BatchName;
            housingBatch.BatchOccupancy = (int)apiBatch.BatchOccupancy;
            housingBatch.BatchSkill = apiBatch.BatchSkill;
            housingBatch.Location = apiBatch.Location;
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
        /// <returns>
        /// New instance of Batch that has been created with the ApiBatchs properties
        /// </returns>
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
                Location = apiBatch.Location
            };

            return batch;
        }
    }
}