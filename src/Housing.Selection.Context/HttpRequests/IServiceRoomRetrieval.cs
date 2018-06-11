using System;
using System.Net.Http;
using System.Collections.Generic;
using Housing.Selection.Library.ServiceHubModels;
using System.Threading.Tasks;

namespace Housing.Selection.Context.HttpRequests
{
    /// <summary>
    /// This interface defines the methods for retrieving room information from the service hub.
    /// </summary>
    public interface IServiceRoomRetrieval
    {
        /// <summary>
        /// Asynchronously retrieves a single room from the service hub api.
        /// </summary>
        /// <returns>
        /// A single room object is returned, or if not found, null is returned.
        /// </returns>
        Task<ApiRoom> RetrieveRoomAsync(Guid guid);

        /// <summary>
        /// Asynchronously etrieves all rooms from the service api.
        /// </summary>
        /// <returns>
        /// Returns a list of Rooms.
        /// </returns>
        Task<List<ApiRoom>> RetrieveAllRoomsAsync();
    }
}