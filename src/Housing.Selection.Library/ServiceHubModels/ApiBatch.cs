using System;
using System.Collections.Generic;

namespace Housing.Selection.Library.ServiceHubModels
{
    public class ApiBatch
    {
        /// <value> Unique batch id </value>
        public Guid BatchId { get; set; }
        /// <value> Start date of training </value>
        public DateTime? StartDate { get; set; }
        /// <value> Expected training end date </value>
        public DateTime? EndDate { get; set; }
        /// <value> Specific name of batch </value>
        public string BatchName { get; set; }
        /// <value> Total number of people in batch </value>
        public int? BatchOccupancy { get; set; }
        /// <value> Batch technology stack </value>
        public string BatchSkill { get; set; }
        /// <value> Location where training takes place </value>
        public string Location { get; set; }
        /// <value> List of associate ids in batch </value>
        public List<Guid> UserIds { get; set; }
    }
}
