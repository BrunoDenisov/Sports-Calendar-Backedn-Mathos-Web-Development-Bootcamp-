using SportCalendar.Model;
using SportCalendar.RepositoryCommon;
using SportCalendar.ServiceCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportCalendar.Service
{
    public class UserService : IUserService
    {
        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }
        protected IUserRepository UserRepository { get; set; }
        public async Task<List<User>> GetAllAsync()
        {
            List<User> usersList = await UserRepository.GetAllAsync();
            if(usersList != null)
            {
                return usersList;
            }
            return null;
        }
        public async Task<User> GetByUsernameAsync(string username)
        {
            User result = await UserRepository.GetByUsernameAsync(username);

            if(result != null)
            {
                return result;
            };
            return null;
        }
        public async Task<List<User>> InsertUserAsync(User newUser)
        {
            return null;
        }
        public async Task<List<User>> UpdateUserAsync(string username, User updateUser)
        {
            return null;
        }
        public async Task<List<User>> DeleteUserAsync(string username)
        {
            return null;
        }
    }
}
