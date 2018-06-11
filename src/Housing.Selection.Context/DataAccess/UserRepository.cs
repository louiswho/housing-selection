
using Housing.Selection.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Housing.Selection.Context.DataAccess
{

    /// <summary>
    /// Add, read, update and get by Id Revature users from Housing-Selection database.
    /// </summary>
    /// 
    public class UserRepository : IUserRepository
    {

   
        private List<User> users = new List<User>();

        public UserRepository()
        {         

        }
        public void AddUser(User user)
        {
            users.Add(user);
        }

        public User GetUserById(Guid id)
        {
            return users.First(x => x.Id == id);
        }

        public User GetUserByUserId(Guid userId)
        {
            return users.First(x => x.UserId == userId);
        }

        public IEnumerable<User> GetUsers()
        {
            return users;
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
