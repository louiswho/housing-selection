using Housing.Selection.Library.ServiceHubModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Housing.Selection.Context.ServiceHubProxies
{
    interface IServiceUserRetrievalProxy {

        Task<IEnumerable<ApiUser>> RetrieveAllUsersAsync();
    }
}
