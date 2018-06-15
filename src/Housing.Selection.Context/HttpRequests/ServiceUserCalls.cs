using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Context.HttpRequests
{
    /// <summary>
    /// This class retrieves User information from the service hub.
    /// </summary>
    public class ServiceUserCalls : IServiceUserCalls
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
        /// ServiceUserRetrieval batchCall = new ServiceUserRetrieval(new HttpClientWrapper(new HttpClient()), new ApiPathBuilder());
        /// </example>
        public ServiceUserCalls(IHttpClientWrapper httpClient, IApiPathBuilder apiPath)
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
                var users = new List<ApiUser>();
                var response = await Client.GetAsync(ApiPath.GetUserServicePath());
                if (response.IsSuccessStatusCode)
                {
                    users = await response.Content.ReadAsAsync<List<ApiUser>>();
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

        public async Task UpdateUserAsync(ApiUser user)
        {
            try
            {
                var response = await Client.PutAsync<ApiUser>(ApiPath.GetUserServicePath(), user);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Update failed for " + user.UserId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
