using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Selection.Library.ViewModels
{
    public class RoomViewModel
    {
        public Guid id { get; set; }
        
        public string location { get; set; }
        
        public int vacancy { get; set; }
        
        public int occupancy { get; set; }
        
        public string gender { get; set; }
        
        public AddressViewModel address { get; set; }

        public ICollection<UserViewModel> users { get; set; }
    }
}
