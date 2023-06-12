using SportCalendar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportCalendar.ServiceCommon
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByUserIdAsync(Guid id);
        Task<User> InsertUserAsync(User newUser);
        Task<User> UpdateUserAsync(Guid id, User updateUser);
        Task<User> DeleteUserAsync(Guid id);
    }
}
