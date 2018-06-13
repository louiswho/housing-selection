using System.Collections.Generic;
using Housing.Selection.Library;
using Housing.Selection.Library.HousingModels;
using Housing.Selection.Library.ViewModels;

namespace Housing.Selection.Context.Selection
{
    public interface ISelectionService
    {
        List<Batch> GetBatches();
        List<Room> GetRooms();
        List<User> GetUsers();
        IEnumerable<Room> CustomSearch(RoomSearchViewModel roomSearchViewModel);
        void AddUserToRoom(AddRemoveUserFromRoomModel addRemoveUserFromRoomModel);
        void RemoveUserFromRoom(AddRemoveUserFromRoomModel addRemoveUserFromRoomModel);
    }
}
