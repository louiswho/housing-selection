using Housing.Selection.Library.HousingModels;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Context.Polling
{
    public interface IPollingService
    {
        void PollAll(); 
        Batch UpdateBatch(ApiBatch apiBatch);
        Room UpdateRoom(ApiRoom apiRoom);
        User UpdateUser(ApiUser apiUser); 
    }
}