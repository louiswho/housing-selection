using Housing.Selection.Library;
using System.Collections.Generic;
using Housing.Selection.Library.HousingModels;

namespace Housing.Selection.Context
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
