using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Context.HttpRequests
{
    public class ServiceRoomRetrieval : IServiceRoomRetrieval
    {
        public IHttpClientWrapper Client { get; set; }
        public IApiPathBuilder ApiPath { get; set; }

        /// <summary>
        /// This is the constructor, where the HttpClient and ApiPathBuilder is injected.
        /// </summary>
        /// <remarks>
        /// For IApiPathBuilder, you only need to pass a new ApiPathBuilder object.
        /// If any changes to paths need to be made, do it in the ApiPathBuilder base class.
        /// </remarks>
        public ServiceRoomRetrieval(IHttpClientWrapper httpClient, IApiPathBuilder apiPath)
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
                var response = new HttpResponseWrapper(await Client.GetAsync(ApiPath.GetRoomServicePath()));
                if (response.IsSuccessStatusCode())
                {
                    rooms = await response.ReadAsAsync<List<ApiRoom>>();
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
