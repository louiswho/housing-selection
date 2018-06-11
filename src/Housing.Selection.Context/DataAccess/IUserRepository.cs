using Housing.Selection.Library;
using System;
using System.Collections.Generic;
using System.Text;
using Housing.Selection.Library.HousingModels;

namespace Housing.Selection.Context.DataAccess
{
    /// <summary>
    /// Creates, reads and updates  Revature users from Housing-Selection database.
    /// </summary>
    
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserById(Guid id);
        User GetUserByUserId(Guid userId);
        void AddUser(User user);
        void SaveChanges();
    }
}
