using System;

namespace Housing.Selection.Library.ServiceHubModels

{
    public class ApiUser
    {
        public Guid UserId { get; set; }
        public string Location { get; set; }
        public ApiAddress Address { get; set; }
        public string Email { get; set; }
        public ApiName Name { get; set; }
        public char Gender { get; set; }
        public string Type { get; set; }
    }
}
