using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Context.HttpRequests
{
    public class ServiceBatchRetrieval : IServiceBatchRetrieval
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
                List<ApiBatch> batches = new List<ApiBatch>();
                IHttpResponseWrapper response = new HttpResponseWrapper(await Client.GetAsync(ApiPath.GetBatchServicePath()));
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
