using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Context.HttpRequests
{
    public class ServiceUserRetrieval : IServiceUserRetrieval
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
        public ServiceUserRetrieval(IHttpClientWrapper httpClient, IApiPathBuilder apiPath)
        {
            Client = httpClient;
            ApiPath = apiPath;
        }


        /// <summary>
        /// Asynchronously retrieves all service hub users.
        /// </summary>
        /// <returns>
        /// Returns a List<ApiUser>.
        /// </returns>
        public async Task<List<ApiUser>> RetrieveAllUsersAsync()
        {
            try
            {
                List<ApiUser> users = new List<ApiUser>();
                IHttpResponseWrapper response = new HttpResponseWrapper(await Client.GetAsync(ApiPath.GetUserServicePath()));
                if (response.IsSuccessStatusCode())
                {
                    users = await response.ReadAsAsync<List<ApiUser>>();
                    if (users.Count <= 0) return null;
                    return users;
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
