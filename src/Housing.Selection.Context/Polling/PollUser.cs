using System.Collections.Generic;
using System.Threading.Tasks;
using Housing.Selection.Context.DataAccess;
using Housing.Selection.Context.HttpRequests;
using Housing.Selection.Library.HousingModels;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Context.Polling
{
    public class PollUser : IPollUser
    {
        private IUserRepository userRepository;
        private IServiceUserRetrieval userRetrieval;

        public PollUser(IUserRepository userRepository, IServiceUserRetrieval userRetrieval)
        {
            this.userRepository = userRepository;
            this.userRetrieval = userRetrieval;
        }
        public async Task<List<User>> UserPoll()
        {
            var userList = new List<User>();
            var users = await userRetrieval.RetrieveAllUsersAsync();
            if (users != null)
            {
                foreach (var user in users)
                {
                    userList.Add(UpdateUser(user));
                }
            }
            return userList;
        }

        public User UpdateUser(ApiUser user)
        {
            var housingUser = userRepository.GetUserByUserId(user.UserId);
            housingUser = housingUser.ConvertFromServiceModel(apiUser: user);
            userRepository.SaveChanges();
            return housingUser;
        }
    }
}