using System.Collections.Generic;
using Housing.Selection.Library.ServiceHubModels;
using System.Threading.Tasks;

namespace Housing.Selection.Context.HttpRequests
{
    /// <summary>
    /// This interface defines the methods for retrieving Users from the service hub.
    /// </summary>
    public interface IServiceUserCalls
    {
        /// <summary>
        /// This will asynchronously retrieve all users from the service api.
        /// </summary>
        /// <returns>
        /// Returns a list of users.
        /// </returns>
        Task<List<ApiUser>> RetrieveAllUsersAsync();
        Task UpdateUserAsync(ApiUser user);
    }
}