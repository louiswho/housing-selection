using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Housing.Selection.Context.DataAccess;
using Housing.Selection.Context.HttpRequests;
using Housing.Selection.Library.HousingModels;
using Housing.Selection.Library.ServiceHubModels;
using System;

namespace Housing.Selection.Context.Polling
{
    public class PollUser : IPollUser
    {
        private IUserRepository userRepository;
        private IServiceUserCalls userRetrieval;

        private readonly IBatchRepository batchRepository;
        private readonly IServiceBatchCalls batchRetrieval;

        private readonly IRoomRepository roomRepository;
        private readonly IServiceRoomCalls roomRetrieval;

        private IAddressRepository addressRepository;
        private INameRepository nameRepository;

        public PollUser(IUserRepository userRepository, IServiceUserCalls userRetrieval, IAddressRepository addressRepository, INameRepository nameRepository, IBatchRepository batchRepository, IServiceBatchCalls batchRetrieval, IRoomRepository roomRepository, IServiceRoomCalls roomRetrieval)
        {
            this.userRepository = userRepository;
            this.userRetrieval = userRetrieval;
            this.addressRepository = addressRepository;
            this.nameRepository = nameRepository;
            this.batchRepository = batchRepository;
            this.batchRetrieval = batchRetrieval;
            this.roomRepository = roomRepository;
            this.roomRetrieval = roomRetrieval;
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
            var users = await userRetrieval.RetrieveAllUsersAsync();
            var batches = await batchRetrieval.RetrieveAllBatchesAsync();
            var rooms = await roomRetrieval.RetrieveAllRoomsAsync();
            if (users != null)
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
            var housingUser = userRepository.GetUserByUserId(apiUser.UserId);
            if (housingUser == null)
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
            userRepository.SaveChanges();
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
        public Room GetRoomId(ApiUser apiUser, List<ApiRoom> rooms)
        {
            var roomId = (from x in rooms
                          where x.Address.AddressId == apiUser.Address.AddressId
                          select x.RoomId).FirstOrDefault();
            if (roomId != null)
                return roomRepository.GetRoomByRoomId(roomId);
            else
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
        public Batch GetBatchId(ApiUser apiUser, List<ApiBatch> batches)
        {
            var batchId = (from x in batches
                           where x.UserIds.Any(y => y == apiUser.UserId)
                           select x).FirstOrDefault().BatchId;
            if (batchId != null)
                return batchRepository.GetBatchByBatchId(batchId);
            else
                return null;
        }
        public Address UpdateAddress(ApiAddress apiAddress)
        {
            var housingAddress = addressRepository.GetAddressByAddressId(apiAddress.AddressId);
            housingAddress = housingAddress.ConvertFromServiceModel(apiAddress);
            addressRepository.SaveChanges();
            return housingAddress;
        }

        public Name UpdateName(ApiName apiName)
        {
            var housingName = nameRepository.GetNameByNameId(apiName.NameId);
            housingName = housingName.ConvertFromServiceModel(apiName);
            nameRepository.SaveChanges();
            return housingName;
        }

    }
}