using System;
using System.Net.Http;
using System.Collections.Generic;
using Housing.Selection.Library;
using Housing.Selection.Library.ServiceHubModels;
using System.Threading.Tasks;

namespace Housing.Selection.Context.HttpRequests
{
    /// <summary>
    /// This interface defines the methods for retrieving batch information from the service hub.
    /// </summary>
    public interface IServiceBatchRetrieval
    {
        /// <summary>
        /// RetrieveAllBatches will asynchronously retrieve all batches from the service api.
        /// </summary>
        /// <returns>
        /// Returns a list of Batches.
        /// </returns>
        Task<List<ApiBatch>> RetrieveAllBatchesAsync();
    }
}