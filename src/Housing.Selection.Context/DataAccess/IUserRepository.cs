using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Housing.Selection.Library.HousingModels;

namespace Housing.Selection.Context.DataAccess
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByUserId(Guid userId);
        void AddUser(User user);
        Task SaveChangesAsync();
    }
}
