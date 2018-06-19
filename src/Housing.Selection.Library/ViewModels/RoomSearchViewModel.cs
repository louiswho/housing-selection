namespace Housing.Selection.Library.ViewModels
{
    public class RoomSearchViewModel
    {
        public string Location { get; set; }
        public string Batch { get; set; }
        public double BatchMinimumPercentage { get; set; }
        public string Gender { get; set; }
        public bool? IsCompletelyUnassigned { get; set; }
        public bool? HasBedAvailable { get; set; }
    }
}
