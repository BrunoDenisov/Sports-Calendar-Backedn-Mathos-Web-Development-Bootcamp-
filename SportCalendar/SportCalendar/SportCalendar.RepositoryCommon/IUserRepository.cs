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
        Task<User> GetByUserIdAsync(Guid id);
        Task<User> InsertUserAsync(User newUser);
        Task<User> UpdateUserAsync(Guid id, User updateUser);
        Task<User> DeleteUserAsync(Guid id);

        Task<bool> CheckEntryByUserIdAsync(Guid id);
    }
}