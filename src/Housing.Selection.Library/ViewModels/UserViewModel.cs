using System;

namespace Housing.Selection.Library.ViewModels
{
    public class UserViewModel
    {
        public Guid id { get; set; }

        public string location { get; set; }

        public string email { get; set; }

        public string gender { get; set; }

        public string type { get; set; }
        
        public NameViewModel name { get; set; }

        public AddressViewModel address { get; set; }
    }
}
