using System.Collections.Generic;
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
        IEnumerable<User> CustomUserSearch(UserSearchViewModel userSearchViewModel);
        void AddUserToRoom(AddRemoveUserFromRoomModel addRemoveUserFromRoomModel);
        void RemoveUserFromRoom(AddRemoveUserFromRoomModel addRemoveUserFromRoomModel);
    }
}
