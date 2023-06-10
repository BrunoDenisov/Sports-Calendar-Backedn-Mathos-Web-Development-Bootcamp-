using Npgsql;
using SportCalendar.Model;
using SportCalendar.RepositoryCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportCalendar.Repository
{
    public class UserRepository :IUserRepository
    {
        private static string connectionString = Environment.GetEnvironmentVariable("ConnectionString");
        public async Task<List<User>> GetAllAsync()
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString); 
            List<User> usersList = new List<User>();
            using (connection)
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM public.\"User\";";
                NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                
                while(await reader.ReadAsync())
                {
                    usersList.Add(
                        new User()
                        {
                            Id = (Guid)reader["Id"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            Password = (string)reader["Password"],
                            Email = (string)reader["Email"],
                            RoleId = (Guid)reader["RoleId"],
                            IsActive = (bool)reader["IsActive"],
                            UpdatedByUserId = (Guid)reader["UpdatedByUserId"],
                            DateCreated = (DateTime)reader["DateCreated"],
                            DateUpdated = (DateTime)reader["DateUpdated"],
                            Username = (string)reader["Username"]

                        }) ;
                }
            }
            
            
            return usersList;
        }

        public async Task<List<User>> GetByUsernameAsync(string username)
        {
            return null;
        }
        public async Task<List<User>> InsertUserAsync(Guid id, User newUser)
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

        public async Task<bool> CheckEntryByUsernameAsync(string username)
        {
            return false;
        }
    }
}
