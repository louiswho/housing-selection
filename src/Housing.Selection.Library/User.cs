using System;

namespace Housing.Selection.Library
{
    public class User
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Type { get; set; }

        public Batch Batch { get; set; }

        public Room Room { get; set; }

        public Name Name { get; set; }

        public Address Address { get; set; }
    }
}
