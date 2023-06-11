using SportCalendar.Common;
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
        public async Task<User> InsertUserAsync(User newUser)
        {
            bool isUser = await UserRepository.CheckEntryByUsernameAsync(newUser.Username);

            if (!isUser)
            {
                Guid newGuid = Guid.NewGuid();

                string hashPassword = PasswordHasher.HashPassword(newUser.Password);
                newUser.Password = hashPassword;

                User result = await UserRepository.InsertUserAsync(newGuid, newUser);

                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }
        public async Task<User> UpdateUserAsync(string username, User updateUser)
        {
            bool isUser = await UserRepository.CheckEntryByUsernameAsync(username);

            if(isUser)
            { if(updateUser.Password != null)
                {
                    string hashPassword = PasswordHasher.HashPassword(updateUser.Password);
                    updateUser.Password = hashPassword;
                }
               
                User result = await UserRepository.UpdateUserAsync(username, updateUser);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }
        public async Task<User> DeleteUserAsync(string username)
        {
            bool isUser = await UserRepository.CheckEntryByUsernameAsync(username);

            if(!isUser)
            {
                User result = await UserRepository.DeleteUserAsync(username);
                return result;
            }
            return null;
        }
    }
}
