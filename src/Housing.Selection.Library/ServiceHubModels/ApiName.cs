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
        
        public static explicit operator Name(ApiName apiName)
        {
            Name name = new Name()
            {
                NameId = apiName.NameId,
                First = apiName.First,
                Middle = apiName.Middle,
                Last = apiName.Last,
                
            };
            return name;
        }
    }
}
