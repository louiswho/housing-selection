using Housing.Selection.Library.ServiceHubModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Housing.Selection.Context.ServiceHubProxies
{
    interface IServiceRoomRetrievalProxy
    {
        Task<IEnumerable<ApiRoom>> RetrieveAllRoomsAsync();
    }
}
