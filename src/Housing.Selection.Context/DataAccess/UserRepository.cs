
using Housing.Selection.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using Housing.Selection.Library.HousingModels;
using Microsoft.EntityFrameworkCore;

namespace Housing.Selection.Context.DataAccess
{

    /// <summary>
    /// Add, read, update and get by Id Revature users from Housing-Selection database.
    /// </summary>
    /// 
    public class UserRepository : IUserRepository
    {
        private readonly IDbContext _HousingSelectionDbContext;

        private List<User> users = new List<User>();

        public UserRepository(IDbContext housingSelectionContext)
        {
            _HousingSelectionDbContext = housingSelectionContext;
        }


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

        public int SaveChanges()
        {
          return  _HousingSelectionDbContext.saveChanges();
        }
    }
}
