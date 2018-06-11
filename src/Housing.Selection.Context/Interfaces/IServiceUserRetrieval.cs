using System;
using System.Net.Http;
using System.Collections.Generic;
using Housing.Selection.Library;
using System.Threading.Tasks;

namespace Housing.Selection.Context.Interfaces
{
    /// <summary>
    /// This interface defines the methods for retrieving Users from the service hub.
    /// </summary>
    public interface IServiceUserRetrieval
    {
        /// <summary>
        /// Asynchronously retrieves a single user from the service hub api.
        /// </summary>
        /// <returns>
        /// A single user object is returned, or if not found, null is returned.
        /// </returns>
        Task<User> RetrieveUserAsync(Guid guid);

        /// <summary>
        /// This will asynchronously retrieve all users from the service api.
        /// </summary>
        /// <returns>
        /// Returns a list of users.
        /// </returns>
        Task<List<User>> RetrieveAllUsersAsync();
    }
}