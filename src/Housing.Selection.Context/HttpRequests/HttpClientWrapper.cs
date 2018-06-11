using System;
using System.Collections.Generic;
using System.Text;
using Housing.Selection.Context.Interfaces;
using System.Net.Http;

namespace Housing.Selection.Context.HttpRequests
{
    /// <summary>
    /// This provides a wrapper for the HttpClient dependency.
    /// </summary>
    public class HttpClientWrapper : IHttpClientWrapper
    {
        public HttpClient Client { get; set; }

        public HttpClientWrapper()
        {
            Client = new HttpClient();
        }
    }
}
