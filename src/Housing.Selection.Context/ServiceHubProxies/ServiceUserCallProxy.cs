using Housing.Selection.Context.HttpRequests;
using Housing.Selection.Library.ServiceHubModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Housing.Selection.Context.ServiceHubProxies
{
    public class ServiceUserCallProxy :  IServiceUserCalls
    {
        private readonly List<ApiUser> _users;

        public ServiceUserCallProxy()
        {
            _users = new List<ApiUser>
            {
                new ApiUser
                {
                    UserId = Guid.NewGuid(),
                    Email = "Linda@mail.com",
                    Type = "Associate",
                    Name = new ApiName
                    {
                        First = "Linda",
                        Last = "Houdini",
                        NameId = Guid.NewGuid()
                    },
                    Address = new ApiAddress
                    {
                        AddressId = Guid.NewGuid(),
                        Address1 = "13451 N 50th St",
                        City = "Tampa",
                        Country = "USA",
                        PostalCode = "33620"
                    },
                    Gender = 'M',
                    Location = "Tampa"
                },
                new ApiUser
                {
                    UserId = Guid.NewGuid(),
                    Email = "Carly@mail.com",
                    Type = "Associate",
                    Name = new ApiName
                    {
                        First = "Carly",
                        Last = "Houdini",
                        NameId = Guid.NewGuid()
                    },
                    Address = new ApiAddress
                    {
                        AddressId = Guid.NewGuid(),
                        Address1 = "12977 N 50th St",
                        City = "Tampa",
                        Country = "USA",
                        PostalCode = "33620"
                    },
                    Gender = 'F',
                    Location = "Tampa"
                },
                new ApiUser
                {
                    UserId = Guid.NewGuid(),
                    Email = "lary@mail.com",
                    Type = "Associate",
                    Name = new ApiName
                    {
                        First = "Larry",
                        Last = "Loudini",
                        NameId = Guid.NewGuid()
                    },
                    Address = new ApiAddress
                    {
                        AddressId = Guid.NewGuid(),
                        Address1 = "12980 N 40th St",
                        City = "Tampa",
                        Country = "USA",
                        PostalCode = "33620"
                    },
                    Gender = 'M',
                    Location = "Tampa"
                },
                new ApiUser
                {
                    UserId = Guid.NewGuid(),
                    Email = "Mary@mail.com",
                    Type = "Associate",
                    Name = new ApiName
                    {
                        First = "Larry",
                        Last = "Loudini",
                        NameId = Guid.NewGuid()
                    },
                    Address = new ApiAddress
                    {
                        AddressId = Guid.NewGuid(),
                        Address1 = "12773 N 21st St",
                        City = "Tampa",
                        Country = "USA",
                        PostalCode = "33620"
                    },
                    Gender = 'M',
                    Location = "Tampa"
                },
                new ApiUser
                {
                    UserId = Guid.NewGuid(),
                    Email = "Mary@mail.com",
                    Type = "Associate",
                    Name = new ApiName
                    {
                        First = "Mary",
                        Last = "Loudini",
                        NameId = Guid.NewGuid()
                    },
                    Address = new ApiAddress
                    {
                        AddressId = Guid.NewGuid(),
                        Address1 = "12773 N 21st St",
                        City = "Reston",
                        Country = "USA",
                        PostalCode = "45321"
                    },
                    Gender = 'F',
                    Location = "Reston"
                }
            };

        }

        public IEnumerable<Guid> RetrieveUserIds()
        {
            var userIds = new List<Guid>();
            foreach (var user in _users)
            {
                userIds.Add(user.UserId);
            }
            return userIds;
        }


        public Task UpdateUserAsync(ApiUser user)
        {
            return Task.Run(() => this.UpdateUserPrivate(user));
        }

        private void UpdateUserPrivate(ApiUser user)
        {
            if (user.UserId == Guid.Empty) throw new Exception("Update failed for user with UserId " + user.UserId);

            var _user = _users.First(x => x.UserId == user.UserId);
            if (user == null) throw new Exception("Update failed for room with UserId " + user.UserId);
            if( user.Address != null)
            {
                var validate = true;
                validate = (user.Address.AddressId == Guid.Empty) ? false : validate;
                validate = !string.IsNullOrEmpty(user.Address.Address1) && validate;
                validate = !string.IsNullOrEmpty(user.Address.City) && validate;
                validate = !string.IsNullOrEmpty(user.Address.PostalCode) && validate;
                validate = !string.IsNullOrEmpty(user.Address.Country) && validate;
                validate = !string.IsNullOrEmpty(user.Address.State) && validate;

                if (!validate) throw new Exception("Update failed, Address was not valid");
            }

            if (user.Location == "") throw new Exception("Update failed the location is invalid");

            user.Location = _user.Location;
            user.Address = _user.Address;
        }

       public Task<List<ApiUser>> RetrieveAllUsersAsync()
        {
            return Task.FromResult<List<ApiUser>>(_users);
        }
    }
}
