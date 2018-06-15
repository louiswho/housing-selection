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
                if (response.IsSuccessStatusCode)
                {
                    rooms = await response.Content.ReadAsAsync<List<ApiRoom>>();
                    if (rooms.Count <= 0) return null;
                    return rooms;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
