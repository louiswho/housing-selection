using Housing.Selection.Library;
using System.Collections.Generic;
using Housing.Selection.Library.HousingModels;

namespace Housing.Selection.Context
{
    interface ISelectionService
    {
        List<Batch> GetBatches();
        List<Room> GetRooms();
        List<User> GetUsers();
        List<Room> CustomSearch(RoomSearchViewModel roomSearchViewModel);
    }
}
