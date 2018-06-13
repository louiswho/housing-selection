using System.Threading.Tasks;

namespace Housing.Selection.Context.HttpRequests
{
    /// <summary>
    /// This is a wrapper for the HttpResponseMessage
    /// to enable testing of the extension method
    /// that HttpResponseMessage uses.
    /// </summary>
    public interface IHttpResponseWrapper
    {
        bool IsSuccessStatusCode();
        Task<T> ReadAsAsync<T>();
    }
}
