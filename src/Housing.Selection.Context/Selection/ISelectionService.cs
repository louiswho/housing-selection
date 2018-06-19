using System.Collections.Generic;
using System.Threading.Tasks;
using Housing.Selection.Library.HousingModels;
using Housing.Selection.Library.ViewModels;

namespace Housing.Selection.Context.Selection
{
    public interface ISelectionService
    {
        Task<List<Batch>> GetBatches();
        Task<List<Room>> GetRooms();
        Task<List<User>> GetUsers();
        Task<IEnumerable<Room>> CustomSearch(RoomSearchViewModel roomSearchViewModel);
        Task<IEnumerable<User>> CustomUserSearch(UserSearchViewModel userSearchViewModel);
        Task AddUserToRoom(AddRemoveUserFromRoomModel addRemoveUserFromRoomModel);
        Task RemoveUserFromRoom(AddRemoveUserFromRoomModel addRemoveUserFromRoomModel);
    }
}
