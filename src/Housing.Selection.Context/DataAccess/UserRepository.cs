using System;
using System.Collections.Generic;
using System.Linq;
using Housing.Selection.Library.HousingModels;

namespace Housing.Selection.Context.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContext _housingSelectionDbContext;

        public UserRepository(IDbContext housingSelectionContext)
        {
            _housingSelectionDbContext = housingSelectionContext;
        }

        public void AddUser(User user)
        {
            _housingSelectionDbContext.Users.Add(user);
        }

        public User GetUserById(Guid id)
        {
            return _housingSelectionDbContext.Users.First(x => x.Id == id);
        }

        public User GetUserByUserId(Guid userId)
        {
            return _housingSelectionDbContext.Users.First(x => x.UserId == userId);
        }

        public IEnumerable<User> GetUsers()
        {
            return _housingSelectionDbContext.Users;
        }
        
        public void SaveChanges()
        {
            _housingSelectionDbContext.SaveChanges();
        }
    }
}
