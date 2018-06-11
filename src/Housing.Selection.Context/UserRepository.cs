using Housing.Selection.Context.Interfaces;
using Housing.Selection.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Housing.Selection.Library;

namespace Housing.Selection.Context.Repositories
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
            return users.First(x => x.UserId == id);
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
