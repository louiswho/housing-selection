using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Selection.Library.ViewModels
{
    public class BatchViewModel
    {
        public Guid id { get; set; }
        
        public DateTime startDate { get; set; }
        
        public DateTime endDate { get; set; }
        
        public string batchName { get; set; }
        
        public int batchOccupancy { get; set; }
        
        public string batchSkill { get; set; }

        public IEnumerable<UserViewModel> users { get; set; }
    }
}
