using System.Collections.Generic;
using System.Threading.Tasks;
using Housing.Selection.Library.HousingModels;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Context.Polling
{
    public interface IPollUser
    {
        Task<List<User>> UserPollAsync();
        Task<User> UpdateUserAsync(ApiUser user, List<ApiBatch> batches, List<ApiRoom> rooms);
    }
}