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

        /// <summary>
        /// Use this method to update a housing Name with the properties from the
        /// calling service hub Name
        /// doesnt change any nav properties from the passed in Name
        /// </summary>
        /// <param name="oldName">An Name object is passed into this method.
        /// Updates the housing Name properties to match the ones grabbed from the
        /// api call.
        /// All other fields are ignored.
        /// </param>
        /// <returns>
        /// Name that has been updated with the calling ApiName's properties
        /// </returns>
        public Name ConvertToName(Name oldName)
        {
            oldName.NameId = this.NameId;
            oldName.First = this.First;
            oldName.Middle = this.Middle;
            oldName.Last = this.Last;

            return oldName;
        }
    }
}
