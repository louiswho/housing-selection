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
        /// This is the constructor, where the HttpClient is injected.
        /// </summary>
        /// <remarks>
        /// For IApiPathBuilder, you only need to pass a new ApiPathBuilder object.
        /// If any changes to paths need to be made, do it in the ApiPathBuilder base class.
        /// </remarks>
        public ServiceBatchRetrieval(IHttpClientWrapper httpClient, IApiPathBuilder apiPath)
        {
            Client = httpClient;
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
                HttpResponseMessage response = await Client.GetAsync(ApiPath.GetBatchServicePath());
                if (response.IsSuccessStatusCode)
                {
                    batches = await response.Content.ReadAsAsync<List<ApiBatch>>();
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

        /// <summary>
        /// Asynchronously retrieves an ApiBatch by id.
        /// </summary>
        /// <returns>
        /// Returns an ApiBatch if found, null if not found.
        /// </returns>
        public async Task<ApiBatch> RetrieveBatchAsync(Guid guid)
        {
            try
            {
                ApiBatch batch = new ApiBatch();

                HttpResponseMessage response = await Client.GetAsync(ApiPath.GetBatchServicePath(guid));
                if (response.IsSuccessStatusCode)
                {
                    batch = await response.Content.ReadAsAsync<ApiBatch>();
                    return batch;
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
