﻿using SportCalendar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SportCalendar.RepositoryCommon
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByUsernameAsync(string username);
        Task<User> InsertUserAsync(Guid id, User newUser);
        Task<User> UpdateUserAsync(string username, User updateUser);
        Task<User> DeleteUserAsync(string username);

        Task<bool> CheckEntryByUsernameAsync(string username);
    }
}
