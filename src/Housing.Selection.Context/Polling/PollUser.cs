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

        public User UpdateUser(ApiUser user, List<ApiBatch> batches, List<ApiRoom> rooms)
        {
            var housingUser = userRepository.GetUserByUserId(user.UserId);
            if (housingUser == null)
            {
                housingUser = housingUser.NewUserFromServiceModel(user);
            }
            else
            {
                housingUser = housingUser.ConvertFromServiceModel(apiUser: user);
                housingUser.Batch = GetBatchId(user, batches);
                housingUser.Room = GetRoomId(user, rooms);
                housingUser.Address = housingUser.Room.Address;
            }
            userRepository.SaveChanges();
            return housingUser;
        }

        public Room GetRoomId(ApiUser user, List<ApiRoom> rooms)
        {
            var roomId = (from x in rooms
                          where x.Address.AddressId == user.Address.AddressId
                          select x.RoomId).FirstOrDefault();
            if(roomId != null)
                return roomRepository.GetRoomByRoomId(roomId);
            else
                return null;
        }

        public Batch GetBatchId(ApiUser user, List<ApiBatch> batches)
        {
            var batchId = (from x in batches
                           where x.UserIds.Any(y => y == user.UserId)
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