using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Context.HttpRequests
{
    /// <summary>
    /// This class retrieves room information from the service hub.
    /// </summary>
    public class ServiceRoomCalls : IServiceRoomCalls
    {
        public IHttpClientWrapper Client { get; set; }
        public IApiPathBuilder ApiPath { get; set; }

        /// <summary>
        /// This is the constructor, where the HttpClientWrapper and ApiPathBuilder is injected.
        /// </summary>
        /// <param name="apiPath">
        /// Pass in the implementation of IApiPathBuilder.
        /// Api paths to the service hub are hard-wired
        /// into the ApiPathBuilder class.
        /// </param>
        /// <param name="httpClient">
        /// This parameter takes in an IHttpClientWrapper.
        /// The HttpClientWrapper passes in an HttpClient
        /// Object in its own constructor.
        /// </param>
        /// <example>
        /// ServiceRoomRetrieval roomCall = new ServiceRoomRetrieval(new HttpClientWrapper(new HttpClient()), new ApiPathBuilder());
        /// </example>
        public ServiceRoomCalls(IHttpClientWrapper httpClient, IApiPathBuilder apiPath)
        {
            Client = httpClient;
            ApiPath = apiPath;
        }

        /// <summary>
        /// Asynchronously retrieves all service hub rooms.
        /// </summary>
        /// <returns>
        /// Returns a List<ApiRoom>.
        /// </returns>
        public async Task<List<ApiRoom>> RetrieveAllRoomsAsync()
        {
            try
            {
                var rooms = new List<ApiRoom>();
                var response = await Client.GetAsync(ApiPath.GetRoomServicePath());

                rooms = (response.IsSuccessStatusCode) ?
                    rooms = await response.Content.ReadAsAsync<List<ApiRoom>>() : rooms;

                return (rooms.Count > 0) ? rooms : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Makes a call to the Service Hub room service and
        /// updates a room.
        /// </summary>
        /// <param name="room">This is the room to be updated.
        /// The room must have the ID of the room to be updated,
        /// but the object only requires fields that need to be
        /// updated.  Any field that will be left unmodified
        /// can be left null.
        /// </param>
        public async Task UpdateRoomAsync(ApiRoom room)
        {
            try
            {
                if (room.RoomId == Guid.Empty) throw new Exception("Room did not have a valid ID");

                var response = await Client.PutAsync<ApiRoom>(ApiPath.GetRoomServicePath(), room);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Update failed for " + room.RoomId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
