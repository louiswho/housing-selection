using Housing.Selection.Context.HttpRequests;


namespace Housing.Selection.Context.Polling
{
    /// <summary>
    ///  Polls Service Hubs database and updates our records accordingly
    /// </summary>
    public class PollingService : IPollingService, IHttpClientWrapper
    {
        private IHttpClientWrapper httpClient;
        public PollingService(IHttpClientWrapper httpClient)
        {
            this.httpClient = httpClient;
        }
        /// <summary>
        ///  Poll all Service Hub databases through their respective API's and updates our databases with the new data 
        /// </summary>
        public void Poll()
        {
            var batches = httpClient.GetBatches();
            var rooms = httpClient.GetRooms();
            var users = httpClient.GetUsers();
        }
    }
}