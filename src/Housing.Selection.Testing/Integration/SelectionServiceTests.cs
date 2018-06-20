using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Housing.Selection.Context.DataAccess;
using Housing.Selection.Context.Polling;
using Housing.Selection.Context.Selection;
using Housing.Selection.Context.ServiceHubProxies;

namespace Housing.Selection.Testing.Integration
{
    public class SelectionServiceTests
    {
        private readonly SelectionService _selectionService;

        public SelectionServiceTests()
        {
            var context = new HousingSelectionDbContext(IntegrationHelpers.ResolveOptions());

            //_selectionService = new SelectionService(new UserRepository(context), new RoomRepository(context), new BatchRepository(context), 
            //    new ServiceRoomCallProxy(), new ServiceUserCallProxy(), new Mapper(), new PollBatch(), );
        }
    }
}
