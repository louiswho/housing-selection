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

                users = (response.IsSuccessStatusCode) ?
                    await response.Content.ReadAsAsync<List<ApiUser>>() : users;

                return (users.Count > 0) ? users : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Use this method to make a call to the
        /// service hub Users, and update a user.
        /// </summary>
        /// <param name="user">An ApiUser object is passed into this method.
        /// The userID is a required field.
        /// Null address will clear an address.
        /// An address must be a complete, valid address, or else a 400 error will return.
        /// Null location will not update.
        /// If an empty string is passed into location, a 400 error will be returned.
        /// Location can be 1-255 characters.
        /// All other fields are ignored.
        /// </param>
        public async Task UpdateUserAsync(ApiUser user)
        {
            try
            {
                if (user.UserId == Guid.Empty) throw new Exception("No ID provided.");

                if (user.Address != null)
                {
                    var validate = true;
                    validate = string.IsNullOrEmpty(user.Address.Address1) ? false : validate;
                    validate = (user.Address.AddressId != Guid.Empty) && validate;
                    validate = !string.IsNullOrEmpty(user.Address.City) && validate;
                    validate = !string.IsNullOrEmpty(user.Address.State) && validate;
                    validate = !string.IsNullOrEmpty(user.Address.PostalCode) && validate;
                    validate = !string.IsNullOrEmpty(user.Address.Country) && validate;

                    if (!validate) throw new Exception("Address was not valid.");
                }

                if (user.Location == "") throw new Exception("Location was invalid.");

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
