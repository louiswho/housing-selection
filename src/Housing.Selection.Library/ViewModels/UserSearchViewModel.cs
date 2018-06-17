using System;

namespace Housing.Selection.Library.ViewModels
{
    public class UserSearchViewModel
    {
        public string Gender { get; set; }
        public string Location { get; set; }
        public Guid? Batch { get; set; }
        public bool Assigned { get; set; }
    }
}
