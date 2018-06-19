using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Housing.Selection.Context.DataAccess;
using Housing.Selection.Context.HttpRequests;
using Housing.Selection.Library.HousingModels;
using Housing.Selection.Library.ServiceHubModels;
using Housing.Selection.Library.ViewModels;

namespace Housing.Selection.Context.Selection
{ 
    public class SelectionService : ISelectionService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IBatchRepository _batchRepository;
        private readonly IServiceRoomCalls _serviceRoomCalls;
        private readonly IServiceUserCalls _serviceUserCalls;
        private readonly IMapper _mapper;

        public SelectionService(IUserRepository users, IRoomRepository rooms, IBatchRepository batches, IServiceRoomCalls roomCalls, IServiceUserCalls userCalls, IMapper mapper)
        {
            _userRepository = users;
            _roomRepository = rooms;
            _batchRepository = batches;
            _serviceRoomCalls = roomCalls;
            _serviceUserCalls = userCalls;
            _mapper = mapper;
        }
        public async void AddUserToRoom(AddRemoveUserFromRoomModel addRemoveUserFromRoomModel)
        {
            var newUser = await _userRepository.GetUserByUserId(addRemoveUserFromRoomModel.UserId);
            var addRoom = await _roomRepository.GetRoomByRoomId(addRemoveUserFromRoomModel.RoomId);

            newUser.Room = addRoom;
            newUser.Address = addRoom.Address;
            addRoom.Vacancy--;

            await _userRepository.SaveChanges();
            await _roomRepository.SaveChanges();

            var apiUser = _mapper.Map<ApiUser>(newUser);
            var apiRoom = _mapper.Map<ApiRoom>(addRoom);

            await _serviceUserCalls.UpdateUserAsync(apiUser);
            await _serviceRoomCalls.UpdateRoomAsync(apiRoom);
        }

        public IEnumerable<Room> CustomSearch(RoomSearchViewModel roomSearchViewModel)
        {
            var returnedRooms = _roomRepository.GetRooms().ToList();

            var filters = FilterFactories.ResolveAllFilters();

            filters.FilterRequest(ref returnedRooms, roomSearchViewModel);

            return returnedRooms;
        }

        public IEnumerable<User> CustomUserSearch(UserSearchViewModel userSearchViewModel)
        {
            var returnedUsers = _userRepository.GetUsers().ToList();

            var userFilters = UserFilterFactory.ResolveUserFilters();

            userFilters.FilterRequest(ref returnedUsers, userSearchViewModel);

            return returnedUsers;
        }

        public List<Batch> GetBatches()
        {
             return _batchRepository.GetBatches().ToList();
        }

        public List<Room> GetRooms()
        {
            return _roomRepository.GetRooms().ToList();
        }

        public List<User> GetUsers()
        {
            return _userRepository.GetUsers().ToList();
        }

        public async void RemoveUserFromRoom(AddRemoveUserFromRoomModel addRemoveUserFromRoomModel)
        {
            var removeUser = await _userRepository.GetUserByUserId(addRemoveUserFromRoomModel.UserId);
            var emptiedRoom = await _roomRepository.GetRoomByRoomId(addRemoveUserFromRoomModel.RoomId);

            removeUser.Room = null;
            removeUser.Address = null;
            emptiedRoom.Vacancy++;

            await _userRepository.SaveChanges();
            await _roomRepository.SaveChanges();

            var apiUser = _mapper.Map<ApiUser>(removeUser);
            var apiRoom = _mapper.Map<ApiRoom>(emptiedRoom);

            await _serviceUserCalls.UpdateUserAsync(apiUser);
            await _serviceRoomCalls.UpdateRoomAsync(apiRoom);
        }
    }
}
