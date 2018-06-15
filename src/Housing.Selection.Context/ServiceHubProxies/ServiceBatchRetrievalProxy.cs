using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Housing.Selection.Context.HttpRequests;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Context.ServiceHubProxies
{
    public class ServiceBatchRetrievalProxy : IServiceBatchRetrieval
    {
        private List<ApiBatch> _batches;

        public ServiceBatchRetrievalProxy()
        {
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
                    UserIds = this.MakeUserIds(20),
                    Address = new ApiAddress()
                    {
                        AddressId = Guid.NewGuid(),
                        Address1 = "123 1st Street",
                        City = "City",
                        State = "FL",
                        Country = "US",
                        PostalCode = "36984"
                    }
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
                    UserIds = this.MakeUserIds(21),
                    Address = new ApiAddress()
                    {
                        AddressId = Guid.NewGuid(),
                        Address1 = "10837 2nd Street",
                        Address2 = "Room 103",
                        City = "City2",
                        State = "FL",
                        Country = "US",
                        PostalCode = "36985"
                    }
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
                    UserIds = this.MakeUserIds(16),
                    Address = new ApiAddress()
                    {
                        AddressId = Guid.NewGuid(),
                        Address1 = "1st Street",
                        City = "City",
                        State = "FL",
                        Country = "US",
                        PostalCode = "36984"
                    }
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
                    UserIds = this.MakeUserIds(19),
                    Address = new ApiAddress()
                    {
                        AddressId = Guid.NewGuid(),
                        Address1 = "9632 1st Street",
                        Address2 = "Revature Place",
                        City = "CityCity",
                        State = "VA",
                        Country = "US",
                        PostalCode = "36004"
                    }
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

        public async Task<List<ApiBatch>> RetrieveAllBatchesAsync()
        {
            return _batches;
        }
    }
}
