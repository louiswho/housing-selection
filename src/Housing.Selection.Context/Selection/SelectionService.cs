using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Housing.Selection.Context.DataAccess;
using Housing.Selection.Context.HttpRequests;
using Housing.Selection.Context.Polling;
using Housing.Selection.Library.HousingModels;
using Housing.Selection.Library.ServiceHubModels;
using Housing.Selection.Library.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Housing.Selection.Context.Selection
{ 
    public class SelectionService : ISelectionService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IBatchRepository _batchRepository;
        private readonly IServiceRoomCalls _serviceRoomCalls;
        private readonly IServiceUserCalls _serviceUserCalls;
        private readonly IPollBatch _pollBatch;
        private readonly IPollRoom _pollRoom;
        private readonly IPollUser _pollUser;
        private readonly IMapper _mapper;

        public SelectionService(IUserRepository users, IRoomRepository rooms, IBatchRepository batches, IServiceRoomCalls roomCalls, IServiceUserCalls userCalls, IMapper mapper,
            IPollBatch pollBatch, IPollRoom pollRoom, IPollUser pollUser)
        {
            _userRepository = users;
            _roomRepository = rooms;
            _batchRepository = batches;
            _serviceRoomCalls = roomCalls;
            _serviceUserCalls = userCalls;
            _mapper = mapper;
            _pollBatch = pollBatch;
            _pollRoom = pollRoom;
            _pollUser = pollUser;
        }

        private async Task UpdateFromServiceHub()
        {
            await _pollBatch.BatchPollAsync();
            await _pollRoom.RoomPollAsync();
            await _pollUser.UserPollAsync();
        }
        public async Task AddUserToRoom(AddRemoveUserFromRoomModel addRemoveUserFromRoomModel)
        {
            var newUser = await _userRepository.GetUserByUserId(addRemoveUserFromRoomModel.UserId);
            var addRoom = await _roomRepository.GetRoomByRoomId(addRemoveUserFromRoomModel.RoomId);

            newUser.Room = addRoom;
            newUser.Address = addRoom.Address;
            addRoom.Vacancy--;

            await _userRepository.SaveChangesAsync();
            await _roomRepository.SaveChangesAsync();

            var apiUser = _mapper.Map<ApiUser>(newUser);
            var apiRoom = _mapper.Map<ApiRoom>(addRoom);

            await _serviceUserCalls.UpdateUserAsync(apiUser);
            await _serviceRoomCalls.UpdateRoomAsync(apiRoom);
        }

        public async Task<IEnumerable<Room>> CustomSearch(RoomSearchViewModel roomSearchViewModel)
        {
            await UpdateFromServiceHub();

            var returnedRooms = await _roomRepository.GetRooms().AsQueryable().ToListAsync();

            var filters = FilterFactories.ResolveAllFilters();

            filters.FilterRequest(ref returnedRooms, roomSearchViewModel);

            return returnedRooms;
        }

        public async Task<IEnumerable<User>> CustomUserSearch(UserSearchViewModel userSearchViewModel)
        {
            await UpdateFromServiceHub();

            var returnedUsers = await _userRepository.GetUsers().AsQueryable().ToListAsync();

            var userFilters = UserFilterFactory.ResolveUserFilters();

            userFilters.FilterRequest(ref returnedUsers, userSearchViewModel);

            return returnedUsers;
        }

        public async Task<List<Batch>> GetBatches()
        {
            await UpdateFromServiceHub();

            return await _batchRepository.GetBatches().AsQueryable().ToListAsync();
        }

        public async Task<List<Room>> GetRooms()
        {
            await UpdateFromServiceHub();

            return await _roomRepository.GetRooms().AsQueryable().ToListAsync();
        }

        public async Task<List<User>> GetUsers()
        {
            await UpdateFromServiceHub();

            return await _userRepository.GetUsers().AsQueryable().ToListAsync();
        }

        public async Task RemoveUserFromRoom(AddRemoveUserFromRoomModel addRemoveUserFromRoomModel)
        {
            await UpdateFromServiceHub();

            var removeUser = await _userRepository.GetUserByUserId(addRemoveUserFromRoomModel.UserId);
            var emptiedRoom = await _roomRepository.GetRoomByRoomId(addRemoveUserFromRoomModel.RoomId);

            removeUser.Room = null;
            removeUser.Address = null;
            emptiedRoom.Vacancy++;

            await _userRepository.SaveChangesAsync();
            await _roomRepository.SaveChangesAsync();

            var apiUser = _mapper.Map<ApiUser>(removeUser);
            var apiRoom = _mapper.Map<ApiRoom>(emptiedRoom);

            await _serviceUserCalls.UpdateUserAsync(apiUser);
            await _serviceRoomCalls.UpdateRoomAsync(apiRoom);
        }
    }
}
