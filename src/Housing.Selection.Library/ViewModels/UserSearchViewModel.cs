using Housing.Selection.Library.HousingModels;

namespace Housing.Selection.Library.ViewModels
{
    public class UserSearchViewModel
    {
        public string Gender { get; set; }
        public string Location { get; set; }
        public Batch Batch { get; set; }
        public bool Assigned { get; set; }
    }
}
