using System.Collections.Generic;
using System.Linq;
using Housing.Selection.Context.DataAccess;
using Housing.Selection.Library.HousingModels;
using Housing.Selection.Library.ViewModels;

namespace Housing.Selection.Context.Selection
{


    public class SelectionService : ISelectionService
    {
        private readonly IUserRepository userRepo; //Change to _userRepository
        private readonly IRoomRepository roomRepo; //Change to _roomRepository
        private readonly IBatchRepository batchRepo; //Change to _batchRepository

        public SelectionService(IUserRepository _users, IRoomRepository _rooms, IBatchRepository _batches) //Change to users, rooms, and batches.
        {
            userRepo = _users;
            roomRepo = _rooms;
            batchRepo = _batches;
        }
        public void AddUserToRoom(AddRemoveUserFromRoomModel addRemoveUserFromRoomModel)
        {
            var newUser = userRepo.GetUserByUserId(addRemoveUserFromRoomModel.UserId);
            var addRoom = roomRepo.GetRoomByRoomId(addRemoveUserFromRoomModel.RoomId);

            newUser.Room = addRoom;
            addRoom.Users.Add(newUser);

            userRepo.SaveChanges();
            roomRepo.SaveChanges();
        }

        public IEnumerable<Room> CustomSearch(RoomSearchViewModel roomSearchViewModel)
        {
            var returnedRooms = roomRepo.GetRooms().ToList();
            //Change constructors to Factories, see me. 
            AFilter genderFilter = new GenderFilter();
            AFilter locationFilter = new LocationFilter();
            AFilter batchFilter = new BatchFilter();
            AFilter isCompletelyUnassignedFilter = new IsCompletelyUnassignedFilter();

            genderFilter.SetSuccessor(locationFilter);
            locationFilter.SetSuccessor(batchFilter);
            batchFilter.SetSuccessor(isCompletelyUnassignedFilter);

            genderFilter.FilterRequest(ref returnedRooms, roomSearchViewModel);

            return returnedRooms;
        }

        public List<Batch> GetBatches()
        {
            return batchRepo.GetBatches().ToList();
        }

        public List<Room> GetRooms()
        {
            return roomRepo.GetRooms().ToList();
        }

        public List<User> GetUsers()
        {
            return userRepo.GetUsers().ToList();
        }

        public void RemoveUserFromRoom(AddRemoveUserFromRoomModel addRemoveUserFromRoomModel)
        {
            var removeUser = userRepo.GetUserByUserId(addRemoveUserFromRoomModel.UserId);
            var emptiedRoom = roomRepo.GetRoomByRoomId(addRemoveUserFromRoomModel.RoomId);

            removeUser.Room = null;
            emptiedRoom.Users.Remove(removeUser);

            userRepo.SaveChanges();
            roomRepo.SaveChanges();
        }
    }
}
