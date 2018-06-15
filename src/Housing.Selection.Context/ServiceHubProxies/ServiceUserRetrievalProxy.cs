using Housing.Selection.Library.ServiceHubModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Housing.Selection.Context.ServiceHubProxies
{
    public class ServiceUserRetrievalProxy
    {
        public List<ApiUser> _users;

        public ServiceUserRetrievalProxy()
        {
            _users = new List<ApiUser>();

            _users.Add(
                new ApiUser()
                {
                    UserId = Guid.NewGuid(),
                    Email = "Linda@mail.com",
                    Type = "Associate",
                    Name = new ApiName()
                    {
                        First = "Linda",
                        Last = "Houdini",
                        NameId = Guid.NewGuid()
                    },
                    Address = new ApiAddress()
                    {
                        AddressId = Guid.NewGuid(),
                        Address1 = "13451 N 50th St",
                        City = "Tampa",
                        Country = "USA",
                        PostalCode = "33620"
                    },
                    Gender = 'M',
                    Location = "Tampa"
                });
            _users.Add(
            new ApiUser()
            {
                UserId = Guid.NewGuid(),
                Email = "Carly@mail.com",
                Type = "Associate",
                Name = new ApiName()
                {
                    First = "Carly",
                    Last = "Houdini",
                    NameId = Guid.NewGuid()
                },
                Address = new ApiAddress()
                {
                    AddressId = Guid.NewGuid(),
                    Address1 = "12977 N 50th St",
                    City = "Tampa",
                    Country = "USA",
                    PostalCode = "33620"
                },
                Gender = 'F',
                Location = ""
            }
            );
            _users.Add(
             new ApiUser()
             {
                 UserId = Guid.NewGuid(),
                 Email = "lary@mail.com",
                 Type = "Associate",
                 Name = new ApiName()
                 {
                     First = "Larry",
                     Last = "Loudini",
                     NameId = Guid.NewGuid()
                 },
                 Address = new ApiAddress()
                 {
                     AddressId = Guid.NewGuid(),
                     Address1 = "12980 N 40th St",
                     City = "Tampa",
                     Country = "USA",
                     PostalCode = "33620"
                 },
                 Gender = 'M',
                 Location = ""
             }
    );
            _users.Add(
            new ApiUser()
            {
                UserId = Guid.NewGuid(),
                Email = "Mary@mail.com",
                Type = "Associate",
                Name = new ApiName()
                {
                    First = "Larry",
                    Last = "Loudini",
                    NameId = Guid.NewGuid()
                },
                Address = new ApiAddress()
                {
                    AddressId = Guid.NewGuid(),
                    Address1 = "12773 N 21st St",
                    City = "Tampa",
                    Country = "USA",
                    PostalCode = "33620"
                },
                Gender = 'M',
                Location = ""
            }
);
            _users.Add(
               new ApiUser()
               {
                   UserId = Guid.NewGuid(),
                   Email = "Mary@mail.com",
                   Type = "Associate",
                   Name = new ApiName()
                   {
                       First = "Mary",
                       Last = "Loudini",
                       NameId = Guid.NewGuid()
                   },
                   Address = new ApiAddress()
                   {
                       AddressId = Guid.NewGuid(),
                       Address1 = "12773 N 21st St",
                       City = "Tampa",
                       Country = "USA",
                       PostalCode = "33620"
                   },
                   Gender = 'F',
                   Location = ""
               }
);
        }
        public async Task<IEnumerable<ApiUser>> RetrieveUsersAsync()
        {
            return _users;
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
    }
}
