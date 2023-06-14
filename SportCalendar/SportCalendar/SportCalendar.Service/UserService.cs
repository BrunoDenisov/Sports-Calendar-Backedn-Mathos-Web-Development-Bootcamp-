using SportCalendar.Common;
using SportCalendar.Model;
using SportCalendar.RepositoryCommon;
using SportCalendar.ServiceCommon;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace SportCalendar.Service
{
    public class UserService : IUserService
    {
        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }
        protected IUserRepository UserRepository { get; set; }
        public async Task<List<User>> GetAllAsync(Paging paging, Sorting sorting, BaseFiltering filtering)
        {
            List<User> usersList = await UserRepository.GetAllAsync(paging, sorting, filtering);
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


                ClaimsIdentity identity = HttpContext.Current.User.Identity as ClaimsIdentity;
                string userId = identity.FindFirst("Id")?.Value;
                string hashPassword = PasswordHasher.HashPassword(newUser.Password);

                newUser.Id = newGuid;
                newUser.Password = hashPassword;
                newUser.RoleId = Guid.Parse("f81e3cdf-5c78-49b9-a72a-7c12a7e5b814");
                newUser.IsActive = true;
                newUser.UpdatedByUserId = Guid.Parse("0d3fa5c2-684c-4d88-82fd-cea2197c6e86");
                newUser.DateCreated = DateTime.Now;
                newUser.DateUpdated = DateTime.Now;                                             

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
                ClaimsIdentity identity = HttpContext.Current.User.Identity as ClaimsIdentity;
                string userId = identity.FindFirst("Id")?.Value;

                updateUser.UpdatedByUserId = Guid.Parse(userId);                
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
