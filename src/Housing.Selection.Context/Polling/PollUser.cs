using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Housing.Selection.Context.DataAccess;
using Housing.Selection.Context.HttpRequests;
using Housing.Selection.Library.HousingModels;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Context.Polling
{
    /// <summary> 
    /// Polls the Service Hub User database, and updates our(housing) User database 
    /// With the data returned to ensure that our DB is up to date with Service Hubs 
    /// Sets the nav properties for batch and room under the assumption that Batch and
    /// Room poll were run before UserPoll is run to ensure that our data exactly matches
    /// </summary> 
    public class PollUser : IPollUser
    {
        private readonly IUserRepository _userRepository;
        private readonly IServiceUserCalls _userRetrieval;
        private readonly IBatchRepository _batchRepository;
        private readonly IServiceBatchCalls _batchRetrieval;
        private readonly IRoomRepository _roomRepository;
        private readonly IServiceRoomCalls _roomRetrieval;
        private readonly IAddressRepository _addressRepository;
        private readonly INameRepository _nameRepository;

        public PollUser(IUserRepository userRepository, IServiceUserCalls userRetrieval, IAddressRepository addressRepository, INameRepository nameRepository, IBatchRepository batchRepository, IServiceBatchCalls batchRetrieval, IRoomRepository roomRepository, IServiceRoomCalls roomRetrieval)
        {
            _userRepository = userRepository;
            _userRetrieval = userRetrieval;
            _addressRepository = addressRepository;
            _nameRepository = nameRepository;
            _batchRepository = batchRepository;
            _batchRetrieval = batchRetrieval;
            _roomRepository = roomRepository;
            _roomRetrieval = roomRetrieval;
        }

        /// <summary>
        /// Updates the users in the housing user database based on the data retrieved from the service hub database
        /// </summary>
        /// <returns>
        /// Returns a Task<List<User>> that contains the updated user list
        /// </returns>
        public async Task<List<User>> UserPollAsync()
        {
            var userList = new List<User>();
            var users = await _userRetrieval.RetrieveAllUsersAsync();
            var batches = await _batchRetrieval.RetrieveAllBatchesAsync();
            var rooms = await _roomRetrieval.RetrieveAllRoomsAsync();
            if (users != null || batches != null || rooms != null)
            {
                foreach (var user in users)
                {
                    await UpdateAddressAsync(user.Address);
                    await UpdateNameAsync(user.Name);
                    userList.Add(await UpdateUserAsync(user, batches, rooms));
                }
            }
            return userList;
        }

        /// <summary>
        /// Updates a single user in the housing user database based on the user data retrieved from the service hub database
        /// </summary>
        /// <param name="apiUser">
        /// The ApiUser object retrieved from the userRetireval
        /// Contains the properties to update housing's matching user with
        /// </param>        
        /// <param name="rooms">
        /// A list of batches, the apiUser.UserId should map to one of the rooms.Users.UserId
        /// But the room has to be retrieved from our database first because ApiRoom doesnt have nav properties
        /// Used to determine which room the user belongs to
        /// </param>
        /// <returns>
        /// Returns a User that contains the updated properties
        /// </returns>
        public async Task<User> UpdateUserAsync(ApiUser apiUser, List<ApiBatch> batches, List<ApiRoom> rooms)
        {
            var housingUser = await _userRepository.GetUserByUserId(apiUser.UserId);
            if (housingUser == null)
            {
                housingUser = housingUser.NewUserFromServiceModel(apiUser);
            }
            else
            {
                housingUser = housingUser.ConvertFromServiceModel(apiUser: apiUser);
                housingUser.Batch = await GetBatchIdAsync(apiUser, batches);
                housingUser.Room = await GetRoomIdAsync(apiUser, rooms);
                housingUser.Address = housingUser.Room.Address;
            }
            await _userRepository.SaveChangesAsync();
            return housingUser;
        }

        /// <summary>
        /// Gets the RoomId from the roomRepository
        /// </summary>
        /// <param name="apiUser">
        /// The ApiUser object retrieved from the userRetireval
        /// Contains the properties to update housing's matching user with
        /// </param>        
        /// <param name="rooms">
        /// A list of batches, the apiUser.UserId should map to one of the rooms.Users.UserId
        /// But the room has to be retrieved from our database first because ApiRoom doesnt have nav properties
        /// Used to determine which room the user belongs to
        /// <returns>
        /// Returns the Room that contains the apiUser
        /// </returns>
        public async Task<Room> GetRoomIdAsync(ApiUser apiUser, IEnumerable<ApiRoom> rooms)
        {
            var roomId = (from x in rooms
                          where x.Address.AddressId == apiUser.Address.AddressId
                          select x.RoomId).FirstOrDefault();
            return await _roomRepository.GetRoomByRoomId(roomId);
        }

        /// <summary>
        /// Gets the RoomId from the roomRepository
        /// </summary>
        /// <param name="apiUser">
        /// The ApiUser object retrieved from the userRetireval
        /// Contains the properties to update housing's matching user with
        /// </param>        
        /// <param name="batches">
        /// A list of batches, the apiUser.UserId should map to one of the batches.UserIds
        /// Used to determine which batch the user belongs to
        /// </param>
        /// Returns a Batch that contains apiUser
        /// </returns>
        public async Task<Batch> GetBatchIdAsync(ApiUser apiUser, IEnumerable<ApiBatch> batches)
        {
            var batchId = (from x in batches
                           where x.UserIds.Any(y => y == apiUser.UserId)
                           select x).FirstOrDefault().BatchId;

            return await _batchRepository.GetBatchByBatchId(batchId);
        }
        public async Task<Address> UpdateAddressAsync(ApiAddress apiAddress)
        {
            var housingAddress = await _addressRepository.GetAddressByAddressId(apiAddress.AddressId);
            housingAddress = housingAddress.ConvertFromServiceModel(apiAddress);
            await _addressRepository.SaveChangesAsync();
            return housingAddress;
        }

        public async Task<Name> UpdateNameAsync(ApiName apiName)
        {
            var housingName = await _nameRepository.GetNameByNameId(apiName.NameId);
            housingName = housingName.ConvertFromServiceModel(apiName);
            await _nameRepository.SaveChangesAsync();
            return housingName;
        }

    }
}