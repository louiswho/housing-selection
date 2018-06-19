using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Housing.Selection.Context.HttpRequests;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Context.ServiceHubProxies
{
    public class ServiceBatchCallProxy : IServiceBatchCalls
    {
        private List<ApiBatch> _batches;
        public readonly ServiceUserCallProxy serviceUserRetrieval;

        public ServiceBatchCallProxy()
        {
            serviceUserRetrieval = new ServiceUserCallProxy();
            _batches = new List<ApiBatch>();
            _batches.Add
                (
                new ApiBatch()
                {
                    BatchId = Guid.NewGuid(),
                    BatchName = "9001-Jan-01",
                    BatchOccupancy = 20,
                    BatchSkill = ".NET",
                    StartDate = new DateTime(2020, 1, 1),
                    EndDate = new DateTime(2020, 5, 1),
                    UserIds = serviceUserRetrieval.RetrieveUserIds().ToList(),
                    Location = "USF"
                }
            );
            _batches.Add
                (
                new ApiBatch()
                {
                    BatchId = Guid.NewGuid(),
                    BatchName = "9002-Jan-01",
                    BatchOccupancy = 21,
                    BatchSkill = "Java",
                    StartDate = new DateTime(2020, 1, 1),
                    EndDate = new DateTime(2020, 5, 1),
                    UserIds = serviceUserRetrieval.RetrieveUserIds().ToList(),
                    Location = "Tampa"
                }
            );
            _batches.Add
                (
                new ApiBatch()
                {
                    BatchId = Guid.NewGuid(),
                    BatchName = "9003-Feb-01",
                    BatchOccupancy = 16,
                    BatchSkill = ".NET",
                    StartDate = new DateTime(2020, 2, 1),
                    EndDate = new DateTime(2020, 6, 1),
                    UserIds = serviceUserRetrieval.RetrieveUserIds().ToList(),
                    Location = "Reston"
                }
            );
            _batches.Add
                (
                new ApiBatch()
                {
                    BatchId = Guid.NewGuid(),
                    BatchName = "9004-Jan-01",
                    BatchOccupancy = 19,
                    BatchSkill = "Java",
                    StartDate = new DateTime(2020, 3, 1),
                    EndDate = new DateTime(2020, 7, 1),
                    UserIds = serviceUserRetrieval.RetrieveUserIds().ToList(),
                    Location = "Virginia"
                }
            );
        }

        private List<Guid> MakeUserIds(int number)
        {
            List<Guid> ids = new List<Guid>();
            for (int i = 0; i < number; i++)
            {
                ids.Add(Guid.NewGuid());
            }
            return ids;
        }
     
        public Task<List<ApiBatch>> RetrieveAllBatchesAsync()
        {
            return Task.FromResult<List<ApiBatch>>(_batches);
        }
    }
}
