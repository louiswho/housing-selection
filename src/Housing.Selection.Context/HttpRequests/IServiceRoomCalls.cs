using System.Collections.Generic;
using Housing.Selection.Library.ServiceHubModels;
using System.Threading.Tasks;

namespace Housing.Selection.Context.HttpRequests
{
    /// <summary>
    /// This interface defines the methods for retrieving room information from the service hub.
    /// </summary>
    public interface IServiceRoomCalls
    {
        /// <summary>
        /// Asynchronously etrieves all rooms from the service api.
        /// </summary>
        /// <returns>
        /// Returns a list of Rooms.
        /// </returns>
        Task<List<ApiRoom>> RetrieveAllRoomsAsync();
    }
}