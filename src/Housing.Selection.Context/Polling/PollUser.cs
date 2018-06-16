using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Housing.Selection.Context.DataAccess;
using Housing.Selection.Context.HttpRequests;
using Housing.Selection.Library.HousingModels;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Context.Polling
{
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
        public async Task<List<User>> UserPoll()
        {
            var userList = new List<User>();
            var users = await _userRetrieval.RetrieveAllUsersAsync();
            var batches = await _batchRetrieval.RetrieveAllBatchesAsync();
            var rooms = await _roomRetrieval.RetrieveAllRoomsAsync();
            if (users != null) //Inveratable if statement
            {
                foreach (var user in users)
                {
                    UpdateAddress(user.Address);
                    UpdateName(user.Name);
                    userList.Add(UpdateUser(user, batches, rooms));
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
        public User UpdateUser(ApiUser apiUser, List<ApiBatch> batches, List<ApiRoom> rooms)
        {
            var housingUser = _userRepository.GetUserByUserId(apiUser.UserId);
            if (housingUser == null) //Dont you want this to be housingUsing != null?????
            {
                housingUser = housingUser.NewUserFromServiceModel(apiUser);
            }
            else
            {
                housingUser = housingUser.ConvertFromServiceModel(apiUser: apiUser);
                housingUser.Batch = GetBatchId(apiUser, batches);
                housingUser.Room = GetRoomId(apiUser, rooms);
                housingUser.Address = housingUser.Room.Address;
            }
            _userRepository.SaveChanges();
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
        public Room GetRoomId(ApiUser apiUser, List<ApiRoom> rooms) //rooms can be IEnumeerable
        {
            var roomId = (from x in rooms
                          where x.Address.AddressId == apiUser.Address.AddressId
                          select x.RoomId).FirstOrDefault();
            if (roomId != null)  //This expression will always be true
                return _roomRepository.GetRoomByRoomId(roomId);
            return null;
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
        public Batch GetBatchId(ApiUser apiUser, List<ApiBatch> batches) //batches can be an IEnumerable
        {
            var batchId = (from x in batches
                           where x.UserIds.Any(y => y == apiUser.UserId)
                           select x).FirstOrDefault().BatchId;

            if (batchId != null)  //This expression is always true. 
            {
                return _batchRepository.GetBatchByBatchId(batchId);
            }
                
            return null;
        }
        public Address UpdateAddress(ApiAddress apiAddress)
        {
            var housingAddress = _addressRepository.GetAddressByAddressId(apiAddress.AddressId);
            housingAddress = housingAddress.ConvertFromServiceModel(apiAddress);
            _addressRepository.SaveChanges();
            return housingAddress;
        }

        public Name UpdateName(ApiName apiName)
        {
            var housingName = _nameRepository.GetNameByNameId(apiName.NameId);
            housingName = housingName.ConvertFromServiceModel(apiName);
            _nameRepository.SaveChanges();
            return housingName;
        }

    }
}