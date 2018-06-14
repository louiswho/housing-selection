using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Selection.Library.ViewModels
{
    public class AddressViewModel
    {
        public Guid id { get; set; }
        
        public string address1 { get; set; }

        public string address2 { get; set; }
        
        public string city { get; set; }
        
        public string state { get; set; }
        
        public string postalCode { get; set; }
        
        public string country { get; set; }
    }
}
