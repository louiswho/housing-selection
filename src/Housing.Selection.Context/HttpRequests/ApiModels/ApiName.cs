using System;

namespace Housing.Selection.Context.HttpRequests.ApiModels
{
    public class ApiName
    {
        public Guid NameId { get; set; }
        public string First { get; set; }
        public string Middle { get; set; }
        public string Last { get; set; }
    }
}
