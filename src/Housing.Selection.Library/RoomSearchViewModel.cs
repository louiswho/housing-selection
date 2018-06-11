namespace Housing.Selection.Library
{
    class RoomSearchViewModel
    {
        public string Location { get; set; }
        public string Batch { get; set; }
        public int BatchMinimumPercentage { get; set; }
        public char Gender { get; set; }
        public bool IsCompletelyUnassigned { get; set; }
    }
}
