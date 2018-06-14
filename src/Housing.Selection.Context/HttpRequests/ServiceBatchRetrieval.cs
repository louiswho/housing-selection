using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Context.HttpRequests
{
    /// <summary>
    /// This class retrieves Batch information from the service hub.
    /// </summary>
    public class ServiceBatchRetrieval : IServiceBatchRetrieval
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
        /// <param name="client">
        /// This parameter takes in an IHttpClientWrapper.
        /// The HttpClientWrapper passes in an HttpClient
        /// Object in its own constructor.
        /// </param>
        /// <example>
        /// ServiceBatchRetrieval batchCall = new ServiceBatchRetrieval(new HttpClientWrapper(new HttpClient()), new ApiPathBuilder());
        /// </example>
        public ServiceBatchRetrieval(IHttpClientWrapper client, IApiPathBuilder apiPath)
        {
            Client = client;
            ApiPath = apiPath;
        }

        /// <summary>
        /// Asynchronously retrieves all service hub batches.
        /// </summary>
        /// <returns>
        /// Returns a List<ApiBatch>.
        /// </returns>
        public async Task<List<ApiBatch>> RetrieveAllBatchesAsync()
        {
            try
            {
                var batches = new List<ApiBatch>();
                var response = new HttpResponseWrapper(await Client.GetAsync(ApiPath.GetBatchServicePath()));
                if (response.IsSuccessStatusCode())
                {
                    batches = await response.ReadAsAsync<List<ApiBatch>>();
                    if (batches.Count <= 0) return null;
                    return batches;
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
