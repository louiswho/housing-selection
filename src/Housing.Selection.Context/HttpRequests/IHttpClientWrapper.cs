using System.Threading.Tasks;
using System.Net.Http;

namespace Housing.Selection.Context.HttpRequests
{
    /// <summary>
    /// This is a wrapper is so that the HttpClient can be mocked.
    /// </summary>
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> GetAsync(string requestUri);
    }
}
