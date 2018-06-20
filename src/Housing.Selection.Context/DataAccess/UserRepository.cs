using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Housing.Selection.Library.HousingModels;
using Microsoft.EntityFrameworkCore;

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
            _housingSelectionDbContext.SaveChanges();
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _housingSelectionDbContext.Users
                .Include(x => x.Batch)
                .Include(y => y.Room)
                .Include(z => z.Name)
                .Include(k => k.Address)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserByUserId(Guid userId)
        {
            return await _housingSelectionDbContext.Users
                .Include(x => x.Batch)
                .Include(y => y.Room)
                .Include(z => z.Name)
                .Include(k => k.Address)
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public IEnumerable<User> GetUsers()
        {
            return _housingSelectionDbContext.Users
                .Include(x => x.Batch)
                .Include(y => y.Room)
                .Include(z => z.Name)
                .Include(k => k.Address);
        }

        public async Task SaveChangesAsync()
        {
            await _housingSelectionDbContext.SaveChangesAsync();
        }
    }
}
