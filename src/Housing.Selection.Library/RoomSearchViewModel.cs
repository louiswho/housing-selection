namespace Housing.Selection.Library
{
    public class RoomSearchViewModel
    {
        public string Location { get; set; }
        public string Batch { get; set; }
        public double BatchMinimumPercentage { get; set; }
        public char Gender { get; set; }
        public bool IsCompletelyUnassigned { get; set; }
    }
}
