using System.Threading.Tasks;
using System.Net.Http;

namespace Housing.Selection.Context.HttpRequests
{
    /// <summary>
    /// This class wraps the HttpResponseMessage and exposes necessary methods.
    /// </summary>
    public class HttpResponseWrapper : IHttpResponseWrapper
    {
        public HttpResponseMessage Response { get; set; }

        public HttpResponseWrapper(HttpResponseMessage response)
        {
            Response = response;
        }

        /// <summary>
        /// Returns whether the status code is a success.
        /// </summary>
        /// <returns>Returns whether the status code is a success.</returns>
        public bool IsSuccessStatusCode()
        {
            return Response.IsSuccessStatusCode;
        }

        /// <summary>
        /// A wrapper for the ReadAsAsync extension method.
        /// </summary>
        /// <typeparam name="T">This must be the object type to be returned.</typeparam>
        /// <returns>Returns T.</returns>
        public async Task<T> ReadAsAsync<T>()
        {
            return await Response.Content.ReadAsAsync<T>();
        }
    }
}
