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
            if (usersList != null)
            {
                return usersList;
            }
            return null;
        }
        public async Task<User> GetByUserIdAsync(Guid id)
        {
            bool isUser = await UserRepository.CheckEntryByUserIdAsync(id);

            if (isUser)
            {
                User result = await UserRepository.GetByUserIdAsync(id);

                if (result != null)
                {
                    return result;
                };
            }
            return null;
        }
        public async Task<User> InsertUserAsync(User newUser)
        {
            bool isUser = await UserRepository.CheckEntryByUserIdAsync(newUser.Id);

            if (!isUser)
            {
                Guid newGuid = Guid.NewGuid();

                newUser.UpdatedByUserId = Guid.Parse("0d3fa5c2-684c-4d88-82fd-cea2197c6e86");
                newUser.DateCreated = DateTime.Now;
                newUser.DateUpdated = DateTime.Now;
                string hashPassword = PasswordHasher.HashPassword(newUser.Password);
                newUser.Id = newGuid;
                newUser.Password = hashPassword;

                User result = await UserRepository.InsertUserAsync(newUser);

                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }
        public async Task<User> UpdateUserAsync(Guid id, User updateUser)
        {
            bool isUser = await UserRepository.CheckEntryByUserIdAsync(id);

            if (isUser)
            {
                updateUser.UpdatedByUserId = Guid.Parse("0d3fa5c2-684c-4d88-82fd-cea2197c6e86");                
                updateUser.DateUpdated = DateTime.Now;
                if (updateUser.Password != null)
                {
                    string hashPassword = PasswordHasher.HashPassword(updateUser.Password);
                    updateUser.Password = hashPassword;
                }

                User result = await UserRepository.UpdateUserAsync(id, updateUser);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }
        public async Task<User> DeleteUserAsync(Guid id)
        {
            bool isUser = await UserRepository.CheckEntryByUserIdAsync(id);

            if (isUser)
            {
                User result = await UserRepository.DeleteUserAsync(id);
                return result;
            }
            return null;
        }
    }
}
