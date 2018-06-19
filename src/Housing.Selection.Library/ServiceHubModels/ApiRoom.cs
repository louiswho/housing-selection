using System;

namespace Housing.Selection.Library.ServiceHubModels
{
    public class ApiRoom
    {
        public Guid RoomId { get; set; }
        public string Location { get; set; }
        public ApiAddress Address { get; set; }
        public int Vacancy { get; set; }
        public int Occupancy { get; set; }
        public string Gender { get; set; }
    }
}
