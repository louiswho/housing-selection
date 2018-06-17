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

        public Batch ConvertFromServiceModel(ApiBatch apiBatch)
        {
            Batch housingBatch = this;            
            housingBatch.BatchId = apiBatch.BatchId;
            housingBatch.StartDate = (DateTime) apiBatch.StartDate;
            housingBatch.EndDate = (DateTime) apiBatch.EndDate;            
            housingBatch.BatchName = apiBatch.BatchName;
            housingBatch.BatchOccupancy = (int) apiBatch.BatchOccupancy;
            housingBatch.BatchSkill = apiBatch.BatchSkill;
            housingBatch.Location = apiBatch.Location;
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
                Location = apiBatch.Location
            };
            //TODO - Figure out how to handle apiBatch userIds
            return batch;
        }
    }
}