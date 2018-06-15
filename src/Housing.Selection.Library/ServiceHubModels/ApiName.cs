using System;
using Housing.Selection.Library.HousingModels;

namespace Housing.Selection.Library.ServiceHubModels
{
    public class ApiName
    {
        public Guid NameId { get; set; }
        public string First { get; set; }
        public string Middle { get; set; }
        public string Last { get; set; }

        public Name ConvertToName(Name name)
        {
            name.NameId = this.NameId;
            name.First = this.First;
            name.Middle = this.Middle;
            name.Last = this.Last;

            return name;
        }
    }
}
