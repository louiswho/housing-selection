
using Housing.Selection.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using Housing.Selection.Library.HousingModels;
using Microsoft.EntityFrameworkCore;
//Clean up and collapse space around class and remove unncessary namespaces. 
namespace Housing.Selection.Context.DataAccess
{

    /// <summary>
    /// Add, read, update and get by Id Revature users from Housing-Selection database.
    /// </summary>
    /// 
    public class UserRepository : IUserRepository
    {
        private readonly IDbContext _HousingSelectionDbContext;
        //Change name to _housingSelectionDbContext.
        private List<User> users = new List<User>();
        //Remove inmemory list. 
        public UserRepository(IDbContext housingSelectionContext)
        {
            _HousingSelectionDbContext = housingSelectionContext;
        }

        //Fix alignment.
        public void AddUser(User user)
        {
               _HousingSelectionDbContext.Users.Add(user);
        }

        public User GetUserById(Guid id)
        {
            return _HousingSelectionDbContext.Users.First(x => x.Id == id);
        }

        public User GetUserByUserId(Guid userId)
        {
            return _HousingSelectionDbContext.Users.First(x => x.UserId == userId);
        }

        public IEnumerable<User> GetUsers()
        {
            return users;
        }
        //Remove return statement.
        public int SaveChanges()
        {
          return  _HousingSelectionDbContext.saveChanges();
        }
    }
}
