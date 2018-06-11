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

        public ServiceBatchRetrieval (IHttpClientWrapper httpClient, IApiPathBuilder apiPath)
        {
            Client = httpClient;
            ApiPath = apiPath;
        }

        public async Task<List<ApiBatch>> RetrieveAllBatchesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ApiBatch> RetrieveBatchAsync(Guid guid)
        {
            try
            {
                ApiBatch batch = new ApiBatch();

                HttpResponseMessage message = await Client.GetAsync(ApiPath.GetBatchServicePath());
            } catch (Exception ex)
            {
                throw ex;
            }
            throw new NotImplementedException();
        }
    }
}
