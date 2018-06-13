using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace Housing.Selection.Context.HttpRequests
{
    /// <summary>
    /// This provides a wrapper for the HttpClient dependency.
    /// </summary>
    public class HttpClientWrapper : IHttpClientWrapper
    {
        public HttpClient Client { get; set; }

        public HttpClientWrapper(HttpClient httpclient)
        {
            Client = httpclient;
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return await Client.GetAsync(requestUri);
        }
    }
}
