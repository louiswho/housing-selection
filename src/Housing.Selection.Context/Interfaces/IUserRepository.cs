﻿using Housing.Selection.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Selection.Context.Interfaces
{
    /// <summary>
    /// Creates, reads and updates  Revature users from Housing-Selection database.
    /// </summary>
    
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserById(Guid id);
        void AddUser(User user);
        void SaveChanges();

    }
}
